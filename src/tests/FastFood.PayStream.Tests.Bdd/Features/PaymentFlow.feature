Feature: Payment Flow
    As a customer
    I want to create and process a payment
    So that I can complete my order

    Scenario: Customer creates payment and processes successfully
        Given I have a valid order with ID "123e4567-e89b-12d3-a456-426614174000"
        And the order total amount is 100.00
        When I create a payment for this order
        Then the payment should be created with status "NotStarted"
        When I generate a QR code for the payment
        Then the payment should have status "QrCodeGenerated"
        And the payment should have a QR code URL
        When the payment gateway confirms the payment
        Then the payment should have status "Approved"
        And the payment should have an external transaction ID
