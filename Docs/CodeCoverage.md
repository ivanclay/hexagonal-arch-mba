```markdown
# 🧪 Cobertura de Testes com Coverlet e ReportGenerator

Este guia mostra como configurar e gerar relatórios de cobertura de testes em projetos .NET usando Coverlet e ReportGenerator.

## 📦 Instalação dos pacotes

Adicione os seguintes pacotes ao seu projeto de testes:

```xml
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="coverlet.msbuild" Version="6.0.2" />
<PackageReference Include="Microsoft.CodeCoverage" Version="17.11.1" />
```

Você pode instalar via terminal:

```bash
dotnet add package coverlet.collector --version 6.0.2
dotnet add package coverlet.msbuild --version 6.0.2
dotnet add package Microsoft.CodeCoverage --version 17.11.1
```

---

## ▶️ Executando os testes com cobertura

Use o comando abaixo para rodar os testes e gerar o arquivo de cobertura:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

O relatório será gerado em:

```
TestResults/<GUID>/coverage.cobertura.xml
```

---

## 📈 Gerando relatórios visuais

Instale o ReportGenerator como ferramenta global:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### 🔹 Gerar relatório HTML

```bash
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
```

### 🔹 Gerar relatório HTML + Markdown para GitHub

```bash
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:"Html;MarkdownSummaryGithub"
```

---

## 📊 Visualizando o resultado

- Abra `coverage-report/index.html` no navegador para ver o relatório visual
- Use o arquivo `SummaryGithub.md` para incluir na documentação ou no README do projeto


📊 **Cobertura de Testes**
Veja o [sumário da cobertura](SummaryGithub.md) para detalhes por classe e método.


---

## ✅ Dicas

- Execute os comandos a partir da raiz do projeto de testes
- Você pode mover o relatório para a pasta `docs/` se quiser mantê-lo versionado
- Para personalizar o que entra na cobertura, use um arquivo `.runsettings`


```

Se quiser, posso te ajudar a adicionar filtros no `.runsettings`, configurar badges de cobertura ou automatizar esse processo com um script!
