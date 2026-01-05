using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;
using FastFood.PayStream.Application.InputModels;
using FastFood.PayStream.Application.UseCases;
using FastFood.PayStream.Application.Ports;
using FastFood.PayStream.Application.Ports.Parameters;
using FastFood.PayStream.Application.Presenters;
using FastFood.PayStream.Domain.Entities;
using FastFood.PayStream.Domain.Common.Enums;

namespace FastFood.PayStream.Tests.Bdd.StepDefinitions;

[Binding]
public class PaymentFlowSteps
{
    private Guid _orderId;
    private decimal _totalAmount;
    private Payment? _payment;
    private string? _qrCodeUrl;
    private string? _externalTransactionId;
    
    private readonly Mock<IPaymentRepository> _mockRepository;
    private readonly Mock<IPaymentGateway> _mockRealGateway;
    private readonly Mock<IPaymentGateway> _mockFakeGateway;
    private CreatePaymentUseCase? _createPaymentUseCase;
    private GenerateQrCodeUseCase? _generateQrCodeUseCase;
    private PaymentNotificationUseCase? _paymentNotificationUseCase;

    public PaymentFlowSteps()
    {
        _mockRepository = new Mock<IPaymentRepository>();
        _mockRealGateway = new Mock<IPaymentGateway>();
        _mockFakeGateway = new Mock<IPaymentGateway>();
    }

    [Given(@"I have a valid order with ID ""(.*)""")]
    public void GivenIHaveAValidOrderWithId(string orderId)
    {
        _orderId = Guid.Parse(orderId);
    }

    [Given(@"the order total amount is (.*)")]
    public void GivenTheOrderTotalAmountIs(decimal totalAmount)
    {
        _totalAmount = totalAmount;
    }

    [When(@"I create a payment for this order")]
    public async Task WhenICreateAPaymentForThisOrder()
    {
        var presenter = new CreatePaymentPresenter();
        _createPaymentUseCase = new CreatePaymentUseCase(_mockRepository.Object, presenter);
        
        Payment? savedPayment = null;
        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<Payment>()))
            .ReturnsAsync((Payment p) =>
            {
                savedPayment = p;
                return p;
            });
        
        _mockRepository
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => savedPayment);
        
        var input = new CreatePaymentInputModel
        {
            OrderId = _orderId,
            TotalAmount = _totalAmount,
            OrderSnapshot = "{\"code\":\"ORD-123\",\"orderedProducts\":[]}"
        };
        
        var output = await _createPaymentUseCase.ExecuteAsync(input);
        _payment = await _mockRepository.Object.GetByIdAsync(output.PaymentId);
    }

    [Then(@"the payment should be created with status ""(.*)""")]
    public void ThenThePaymentShouldBeCreatedWithStatus(string status)
    {
        _payment.Should().NotBeNull();
        _payment!.Status.ToString().Should().Be(status);
    }

    [When(@"I generate a QR code for the payment")]
    public async Task WhenIGenerateAQrCodeForThePayment()
    {
        var presenter = new GenerateQrCodePresenter();
        _generateQrCodeUseCase = new GenerateQrCodeUseCase(
            _mockRepository.Object,
            _mockRealGateway.Object,
            _mockFakeGateway.Object,
            presenter);
        
        _qrCodeUrl = "https://qr.mercadopago.com/test";
        _mockRealGateway
            .Setup(g => g.GenerateQrCodeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<QrCodeItemModel>>()))
            .ReturnsAsync(_qrCodeUrl);
        
        _mockRepository
            .Setup(r => r.GetByOrderIdAsync(_orderId))
            .ReturnsAsync(_payment);
        
        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Callback<Payment>(p => _payment = p)
            .Returns(Task.CompletedTask);
        
        var input = new GenerateQrCodeInputModel
        {
            OrderId = _orderId,
            FakeCheckout = false
        };
        
        await _generateQrCodeUseCase.ExecuteAsync(input);
    }

    [Then(@"the payment should have status ""(.*)""")]
    public void ThenThePaymentShouldHaveStatus(string status)
    {
        _payment.Should().NotBeNull();
        _payment!.Status.ToString().Should().Be(status);
    }

    [Then(@"the payment should have a QR code URL")]
    public void ThenThePaymentShouldHaveAQrCodeUrl()
    {
        _payment.Should().NotBeNull();
        _payment!.QrCodeUrl.Should().NotBeNullOrWhiteSpace();
        _payment.QrCodeUrl.Should().Be(_qrCodeUrl);
    }

    [When(@"the payment gateway confirms the payment")]
    public async Task WhenThePaymentGatewayConfirmsThePayment()
    {
        var presenter = new PaymentNotificationPresenter();
        _paymentNotificationUseCase = new PaymentNotificationUseCase(
            _mockRepository.Object,
            _mockRealGateway.Object,
            _mockFakeGateway.Object,
            presenter);
        
        _externalTransactionId = "TRX123456";
        var statusResult = new PaymentStatusResult
        {
            IsApproved = true,
            IsRejected = false,
            IsCanceled = false,
            IsPending = false,
            TransactionId = _externalTransactionId
        };
        
        _mockRealGateway
            .Setup(g => g.CheckPaymentStatusAsync(It.IsAny<string>()))
            .ReturnsAsync(statusResult);
        
        _mockRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Payment>()))
            .Callback<Payment>(p => _payment = p)
            .Returns(Task.CompletedTask);
        
        var input = new PaymentNotificationInputModel
        {
            OrderId = _orderId,
            FakeCheckout = false
        };
        
        await _paymentNotificationUseCase.ExecuteAsync(input);
    }

    [Then(@"the payment should have an external transaction ID")]
    public void ThenThePaymentShouldHaveAnExternalTransactionId()
    {
        _payment.Should().NotBeNull();
        _payment!.ExternalTransactionId.Should().NotBeNullOrWhiteSpace();
        _payment.ExternalTransactionId.Should().Be(_externalTransactionId);
    }
}
