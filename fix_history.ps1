# Script para remover senha do histórico do Git
# Remove a senha do appsettings.json do Migrator de TODOS os commits do histórico

$filePath = "src/InterfacesExternas/FastFood.PayStream.Migrator/appsettings.json"
$cleanContent = @"
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
"@

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Removendo senha do histórico do Git" -ForegroundColor Yellow
Write-Host "Arquivo: $filePath" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Criar arquivo temporário com conteúdo limpo (sem BOM, apenas UTF-8)
$tempFile = "temp_clean_appsettings.json"
$utf8NoBom = New-Object System.Text.UTF8Encoding $false
$fullPath = Join-Path (Get-Location) $tempFile
[System.IO.File]::WriteAllText($fullPath, $cleanContent, $utf8NoBom)

Write-Host "Arquivo temporário criado: $tempFile" -ForegroundColor Green
Write-Host ""

# Criar script bash que será usado pelo filter-branch
$bashScript = @"
#!/bin/bash
FILE_PATH="$filePath"
CLEAN_CONTENT='{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}'

if [ -f "$FILE_PATH" ]; then
    echo "$CLEAN_CONTENT" > "$FILE_PATH"
    git add "$FILE_PATH"
fi
"@

$bashScriptFile = "temp_filter_script.sh"
$utf8NoBom = New-Object System.Text.UTF8Encoding $false
[System.IO.File]::WriteAllText((Join-Path (Get-Location) $bashScriptFile), $bashScript, $utf8NoBom)

Write-Host "Script de filtro criado: $bashScriptFile" -ForegroundColor Green
Write-Host ""

# Verificar se Git Bash está disponível
$gitBash = "$env:ProgramFiles\Git\bin\bash.exe"
if (-not (Test-Path $gitBash)) {
    $gitBash = "$env:ProgramFiles(x86)\Git\bin\bash.exe"
}

if (-not (Test-Path $gitBash)) {
    Write-Host "ERRO: Git Bash não encontrado!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Por favor, execute manualmente no Git Bash:" -ForegroundColor Yellow
    Write-Host "  git filter-branch --force --tree-filter 'bash temp_filter_script.sh' --prune-empty --tag-name-filter cat -- --all" -ForegroundColor White
    Write-Host ""
    Remove-Item -Path $tempFile -ErrorAction SilentlyContinue
    exit 1
}

Write-Host "Executando git filter-branch..." -ForegroundColor Yellow
Write-Host "Isso pode demorar dependendo do tamanho do repositório..." -ForegroundColor Cyan
Write-Host ""

# Executar git filter-branch usando Git Bash
$result = & $gitBash -c "cd '$(Get-Location)' && git filter-branch --force --tree-filter 'bash $bashScriptFile' --prune-empty --tag-name-filter cat -- --all" 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "git filter-branch executado com sucesso!" -ForegroundColor Green
    
    # Limpar arquivos temporários
    Write-Host ""
    Write-Host "Limpando arquivos temporários..." -ForegroundColor Yellow
    Remove-Item -Path $tempFile -ErrorAction SilentlyContinue
    Remove-Item -Path $bashScriptFile -ErrorAction SilentlyContinue
    
    # Limpar refs do filter-branch
    Write-Host "Limpando referências do filter-branch..." -ForegroundColor Yellow
    Remove-Item -Path ".git/refs/original/" -Recurse -Force -ErrorAction SilentlyContinue
    git reflog expire --expire=now --all 2>&1 | Out-Null
    git gc --prune=now --aggressive 2>&1 | Out-Null
    
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "Senha removida do histórico do Git!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "IMPORTANTE: Se você já fez push para o repositório remoto," -ForegroundColor Red
    Write-Host "será necessário fazer force push:" -ForegroundColor Red
    Write-Host ""
    Write-Host "  git push origin --force --all" -ForegroundColor Yellow
    Write-Host "  git push origin --force --tags" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "ATENÇÃO: Force push reescreve o histórico." -ForegroundColor Red
    Write-Host "Certifique-se de que todos os colaboradores estão cientes!" -ForegroundColor Red
} else {
    Write-Host ""
    Write-Host "ERRO ao executar git filter-branch!" -ForegroundColor Red
    Write-Host "Exit code: $LASTEXITCODE" -ForegroundColor Red
    Write-Host ""
    Write-Host "Você pode tentar executar manualmente no Git Bash:" -ForegroundColor Yellow
    Write-Host "  git filter-branch --force --tree-filter 'bash temp_filter_script.sh' --prune-empty --tag-name-filter cat -- --all" -ForegroundColor White
}
