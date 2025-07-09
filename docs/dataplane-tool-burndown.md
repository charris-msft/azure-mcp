# Azure MCP Server Dataplane Tool Burndown List

This document provides a prioritized list of Azure .NET SDK packages that could be added to the Azure MCP Server as dataplane tools.

## Methodology

1. **Source**: Based on [Azure SDK for .NET releases](https://azure.github.io/azure-sdk/releases/latest/dotnet.html)
2. **Filtering**: 
   - Only `Azure.*` packages (excluded `Microsoft.*`)
   - Excluded management plane packages (`Azure.ResourceManager.*`, `Azure.Provisioning.*`)
   - Removed packages already implemented in Azure MCP Server
   - GA packages prioritized over Beta packages
   - Beta packages filtered to only those updated since calendar year 2024

## Already Implemented Tools

The following Azure SDK packages are already implemented in the Azure MCP Server:
- `Azure.AI.Projects` - Azure Foundry
- `Azure.Data.AppConfiguration` - Azure App Configuration
- `Azure.Data.Tables` - Azure Data Explorer (related)
- `Azure.Messaging.ServiceBus` - Azure Service Bus
- `Azure.Monitor.Query` - Azure Monitor
- `Azure.ResourceManager.AppConfiguration` - App Configuration management
- `Azure.ResourceManager.Authorization` - RBAC
- `Azure.ResourceManager.CognitiveServices` - AI services management
- `Azure.ResourceManager.CosmosDB` - Cosmos DB management
- `Azure.ResourceManager.Datadog` - Native ISV services
- `Azure.ResourceManager.Grafana` - Grafana management
- `Azure.ResourceManager.Kusto` - Data Explorer management
- `Azure.ResourceManager.OperationalInsights` - Monitor workspace management
- `Azure.ResourceManager.PostgreSql` - PostgreSQL management
- `Azure.ResourceManager.Redis` - Redis management
- `Azure.ResourceManager.RedisEnterprise` - Redis Enterprise management
- `Azure.ResourceManager.Search` - Search management
- `Azure.ResourceManager.Sql` - SQL management
- `Azure.ResourceManager.Storage` - Storage management
- `Azure.Search.Documents` - Azure AI Search
- `Azure.Security.KeyVault.Keys` - Azure Key Vault Keys
- `Azure.Security.KeyVault.Secrets` - Azure Key Vault Secrets
- `Azure.Storage.Blobs` - Azure Storage Blobs

Additionally, core/foundational packages are excluded as they are not relevant for dataplane tools.

## Priority 1: General Availability (GA) Packages

These packages have stable GA releases and should be prioritized for implementation:

### Attestation
- **Package**: `Azure.Security.Attestation`
- **Service**: Attestation
- **GA Version**: 1.0.0
- **Latest GA Date**: 05/07/2021

### Azure Batch - File Staging
- **Package**: `Azure.Batch.FileStaging`
- **Service**: Batch
- **GA Version**: 8.0.1
- **Support Status**: active

### AI Agents Persistent
- **Package**: `Azure.AI.Agents.Persistent`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0
- **Latest GA Date**: 05/15/2025

### Content Safety
- **Package**: `Azure.AI.ContentSafety`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0
- **Latest GA Date**: 12/12/2023

### Content Safety Extension Embedded Text
- **Package**: `Azure.AI.ContentSafety.Extension.Embedded.Text`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0

### Conversational Language Understanding
- **Package**: `Azure.AI.Language.Conversations`
- **Service**: Cognitive Services
- **GA Version**: 1.1.0
- **Latest GA Date**: 06/14/2023

### Document Intelligence
- **Package**: `Azure.AI.DocumentIntelligence`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0
- **Latest GA Date**: 12/17/2024

### Document Translation
- **Package**: `Azure.AI.Translation.Document`
- **Service**: Cognitive Services
- **GA Version**: 2.0.0
- **Latest GA Date**: 11/13/2024

### Form Recognizer
- **Package**: `Azure.AI.FormRecognizer`
- **Service**: Cognitive Services
- **GA Version**: 4.1.0
- **Latest GA Date**: 08/10/2023
- **Support Status**: active

### Health Insights Radiology Insights
- **Package**: `Azure.Health.Insights.RadiologyInsights`
- **Service**: Cognitive Services
- **GA Version**: 1.1.0
- **Latest GA Date**: 06/13/2025

### Image Analysis
- **Package**: `Azure.AI.Vision.ImageAnalysis`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0
- **Latest GA Date**: 10/24/2024

### OpenAI Inference
- **Package**: `Azure.AI.OpenAI`
- **Service**: Cognitive Services
- **GA Version**: 2.1.0
- **Latest GA Date**: 12/06/2024

### Question Answering
- **Package**: `Azure.AI.Language.QuestionAnswering`
- **Service**: Cognitive Services
- **GA Version**: 1.1.0
- **Latest GA Date**: 10/13/2022

### Text Analytics
- **Package**: `Azure.AI.TextAnalytics`
- **Service**: Cognitive Services
- **GA Version**: 5.3.0
- **Latest GA Date**: 06/19/2023
- **Support Status**: active

### Text Translation
- **Package**: `Azure.AI.Translation.Text`
- **Service**: Cognitive Services
- **GA Version**: 1.0.0
- **Latest GA Date**: 05/20/2024

### Communication Call Automation
- **Package**: `Azure.Communication.CallAutomation`
- **Service**: Communication
- **GA Version**: 1.4.0
- **Latest GA Date**: 06/04/2025

### Communication Calling Windows Client
- **Package**: `Azure.Communication.Calling.WindowsClient`
- **Service**: Communication
- **GA Version**: 1.11.1

### Communication Chat
- **Package**: `Azure.Communication.Chat`
- **Service**: Communication
- **GA Version**: 1.4.0
- **Latest GA Date**: 06/24/2025
- **Support Status**: active

### Communication Common
- **Package**: `Azure.Communication.Common`
- **Service**: Communication
- **GA Version**: 1.4.0
- **Latest GA Date**: 06/04/2025
- **Support Status**: active

### Communication Email
- **Package**: `Azure.Communication.Email`
- **Service**: Communication
- **GA Version**: 1.0.1
- **Latest GA Date**: 08/18/2023

### Communication Identity
- **Package**: `Azure.Communication.Identity`
- **Service**: Communication
- **GA Version**: 1.3.1
- **Latest GA Date**: 03/22/2024
- **Support Status**: active

### Communication JobRouter
- **Package**: `Azure.Communication.JobRouter`
- **Service**: Communication
- **GA Version**: 1.0.0
- **Latest GA Date**: 11/17/2023

### Communication Messages
- **Package**: `Azure.Communication.Messages`
- **Service**: Communication
- **GA Version**: 1.1.0
- **Latest GA Date**: 10/18/2024

### Communication Network Traversal
- **Package**: `Azure.Communication.NetworkTraversal`
- **Service**: Communication
- **GA Version**: 1.0.0
- **Latest GA Date**: 02/09/2022
- **Support Status**: deprecated

### Communication Phone Numbers
- **Package**: `Azure.Communication.PhoneNumbers`
- **Service**: Communication
- **GA Version**: 1.4.0
- **Latest GA Date**: 06/20/2025

### Communication Rooms
- **Package**: `Azure.Communication.Rooms`
- **Service**: Communication
- **GA Version**: 1.2.0
- **Latest GA Date**: 03/17/2025

### Communication SMS
- **Package**: `Azure.Communication.Sms`
- **Service**: Communication
- **GA Version**: 1.0.1
- **Latest GA Date**: 05/25/2021
- **Support Status**: active

### Confidential Ledger
- **Package**: `Azure.Security.ConfidentialLedger`
- **Service**: Confidential Ledger
- **GA Version**: 1.3.0
- **Latest GA Date**: 02/05/2024

### Container Registry
- **Package**: `Azure.Containers.ContainerRegistry`
- **Service**: Container Registry
- **GA Version**: 1.2.0
- **Latest GA Date**: 02/07/2025

### DCAP Windows
- **Package**: `Azure.DCAP.Windows`
- **Service**: DCAP
- **GA Version**: 1.2.0
- **Support Status**: active

### Dev Center
- **Package**: `Azure.Developer.DevCenter`
- **Service**: Dev Center
- **GA Version**: 1.0.0
- **Latest GA Date**: 04/03/2024

### Event Grid
- **Package**: `Azure.Messaging.EventGrid`
- **Service**: Event Grid
- **GA Version**: 5.0.0
- **Latest GA Date**: 06/26/2025
- **Support Status**: active

### Event Grid Namespaces
- **Package**: `Azure.Messaging.EventGrid.Namespaces`
- **Service**: Event Grid
- **GA Version**: 1.1.0
- **Latest GA Date**: 05/12/2025

### Resource Management - Event Grid
- **Package**: `Azure.ResourceManager.EventGrid`
- **Service**: Event Grid
- **GA Version**: 1.1.0
- **Latest GA Date**: 03/31/2025

### System Events
- **Package**: `Azure.Messaging.EventGrid.SystemEvents`
- **Service**: Event Grid
- **GA Version**: 1.0.0
- **Latest GA Date**: 06/25/2025

### Event Hubs
- **Package**: `Azure.Messaging.EventHubs`
- **Service**: Event Hubs
- **GA Version**: 5.12.2
- **Latest GA Date**: 06/12/2025
- **Support Status**: active

### Event Hubs - Event Processor
- **Package**: `Azure.Messaging.EventHubs.Processor`
- **Service**: Event Hubs
- **GA Version**: 5.12.2
- **Latest GA Date**: 06/13/2025
- **Support Status**: active

### Resource Management - Event Hubs
- **Package**: `Azure.ResourceManager.EventHubs`
- **Service**: Event Hubs
- **GA Version**: 1.2.0
- **Latest GA Date**: 06/13/2025

### Blob Storage Key Store for .NET Data Protection
- **Package**: `Azure.Extensions.AspNetCore.DataProtection.Blobs`
- **Service**: Extensions
- **GA Version**: 1.5.1
- **Latest GA Date**: 06/23/2025
- **Support Status**: active

### Key Encryptor for .NET Data Protection
- **Package**: `Azure.Extensions.AspNetCore.DataProtection.Keys`
- **Service**: Extensions
- **GA Version**: 1.6.1
- **Latest GA Date**: 06/23/2025
- **Support Status**: active

### Secrets Configuration Provider for .NET
- **Package**: `Azure.Extensions.AspNetCore.Configuration.Secrets`
- **Service**: Extensions
- **GA Version**: 1.4.0
- **Latest GA Date**: 02/07/2025
- **Support Status**: active

### Health Deidentification
- **Package**: `Azure.Health.Deidentification`
- **Service**: Health Deidentification
- **GA Version**: 1.0.0
- **Latest GA Date**: 04/23/2025

### Device Update
- **Package**: `Azure.IoT.DeviceUpdate`
- **Service**: IoT
- **GA Version**: 1.0.0
- **Latest GA Date**: 09/08/2022

### Digital Twins
- **Package**: `Azure.DigitalTwins.Core`
- **Service**: IoT
- **GA Version**: 1.6.0
- **Latest GA Date**: 06/04/2025
- **Support Status**: active

### Key Vault - Administration
- **Package**: `Azure.Security.KeyVault.Administration`
- **Service**: Key Vault
- **GA Version**: 4.6.0
- **Latest GA Date**: 06/18/2025

### Key Vault - Certificates
- **Package**: `Azure.Security.KeyVault.Certificates`
- **Service**: Key Vault
- **GA Version**: 4.8.0
- **Latest GA Date**: 06/17/2025
- **Support Status**: active

### Resource Management - Key Vault
- **Package**: `Azure.ResourceManager.KeyVault`
- **Service**: Key Vault
- **GA Version**: 1.3.2
- **Latest GA Date**: 04/02/2025

### Load Testing
- **Package**: `Azure.Developer.LoadTesting`
- **Service**: Load Testing
- **GA Version**: 1.0.2
- **Latest GA Date**: 01/20/2025

### Metrics Advisor
- **Package**: `Azure.AI.MetricsAdvisor`
- **Service**: Metrics Advisor
- **GA Version**: 1.1.0
- **Latest GA Date**: 08/10/2021

### Azure Remote Rendering
- **Package**: `Azure.MixedReality.RemoteRendering`
- **Service**: Mixed Reality
- **GA Version**: 1.1.0
- **Latest GA Date**: 09/17/2021
- **Support Status**: active

### Mixed Reality Authentication
- **Package**: `Azure.MixedReality.Authentication`
- **Service**: Mixed Reality
- **GA Version**: 1.2.0
- **Latest GA Date**: 09/09/2022
- **Support Status**: active

### Monitor Ingestion
- **Package**: `Azure.Monitor.Ingestion`
- **Service**: Monitor
- **GA Version**: 1.1.2
- **Latest GA Date**: 04/03/2024

### OpenTelemetry Exporter
- **Package**: `Azure.Monitor.OpenTelemetry.Exporter`
- **Service**: Monitor
- **GA Version**: 1.4.0
- **Latest GA Date**: 05/09/2025

### Provisioning
- **Package**: `Azure.Provisioning`
- **Service**: Provisioning
- **GA Version**: 1.2.0
- **Latest GA Date**: 07/08/2025

### Resource Management - Resource Manager
- **Package**: `Azure.ResourceManager`
- **Service**: Resource Manager
- **GA Version**: 1.13.1
- **Latest GA Date**: 04/24/2025

### Schema Registry
- **Package**: `Azure.Data.SchemaRegistry`
- **Service**: Schema Registry
- **GA Version**: 1.4.0
- **Latest GA Date**: 09/18/2024

### Resource Management - Service Bus
- **Package**: `Azure.ResourceManager.ServiceBus`
- **Service**: Service Bus
- **GA Version**: 1.1.0
- **Latest GA Date**: 11/28/2024

### Data Movement
- **Package**: `Azure.Storage.DataMovement`
- **Service**: Storage
- **GA Version**: 12.1.0
- **Latest GA Date**: 02/27/2025

### Data Movement - Blobs
- **Package**: `Azure.Storage.DataMovement.Blobs`
- **Service**: Storage
- **GA Version**: 12.1.0
- **Latest GA Date**: 02/27/2025

### Data Movement - Files Shares
- **Package**: `Azure.Storage.DataMovement.Files.Shares`
- **Service**: Storage
- **GA Version**: 12.1.0
- **Latest GA Date**: 02/27/2025

### Storage - Blobs Batch
- **Package**: `Azure.Storage.Blobs.Batch`
- **Service**: Storage
- **GA Version**: 12.21.0
- **Latest GA Date**: 03/11/2025
- **Support Status**: active

### Storage - Common
- **Package**: `Azure.Storage.Common`
- **Service**: Storage
- **GA Version**: 12.23.0
- **Latest GA Date**: 03/11/2025
- **Support Status**: active

### Storage - Files Data Lake
- **Package**: `Azure.Storage.Files.DataLake`
- **Service**: Storage
- **GA Version**: 12.22.0
- **Latest GA Date**: 03/11/2025
- **Support Status**: active

### Storage - Files Share
- **Package**: `Azure.Storage.Files.Shares`
- **Service**: Storage
- **GA Version**: 12.22.0
- **Latest GA Date**: 03/11/2025
- **Support Status**: active

### Storage - Queues
- **Package**: `Azure.Storage.Queues`
- **Service**: Storage
- **GA Version**: 12.22.0
- **Latest GA Date**: 03/11/2025
- **Support Status**: active

### Resource Management - Web PubSub
- **Package**: `Azure.ResourceManager.WebPubSub`
- **Service**: Web PubSub
- **GA Version**: 1.2.0
- **Latest GA Date**: 06/13/2025

### Web PubSub
- **Package**: `Azure.Messaging.WebPubSub`
- **Service**: Web PubSub
- **GA Version**: 1.6.0
- **Latest GA Date**: 05/08/2025

### Web PubSub Client
- **Package**: `Azure.Messaging.WebPubSub.Client`
- **Service**: Web PubSub
- **GA Version**: 1.0.0
- **Latest GA Date**: 12/31/2023

## Priority 2: Beta Packages (Updated Since 2024)

These packages are in preview/beta but have been updated since 2024, indicating active development:



### Microsoft Playwright Testing
- **Package**: `Azure.Developer.MicrosoftPlaywrightTesting.TestLogger`
- **Service**: Microsoft Playwright Testing
- **Preview Version**: 1.0.0-beta.4
- **First Preview Date**: 10/23/2024

### NUnit ? Microsoft Playwright Testing
- **Package**: `Azure.Developer.MicrosoftPlaywrightTesting.NUnit`
- **Service**: Microsoft Playwright Testing
- **Preview Version**: 1.0.0-beta.4
- **First Preview Date**: 10/23/2024

### Language Text
- **Package**: `Azure.AI.Language.Text`
- **Service**: Cognitive Services
- **Preview Version**: 1.0.0-beta.3
- **First Preview Date**: 08/08/2024

### AI Model Inference
- **Package**: `Azure.AI.Inference`
- **Service**: Cognitive Services
- **Preview Version**: 1.0.0-beta.5
- **First Preview Date**: 08/06/2024

### Compute Batch
- **Package**: `Azure.Compute.Batch`
- **Service**: Batch
- **Preview Version**: 1.0.0-beta.3
- **First Preview Date**: 07/02/2024

### Face
- **Package**: `Azure.AI.Vision.Face`
- **Service**: Cognitive Services
- **Preview Version**: 1.0.0-beta.2
- **First Preview Date**: 05/27/2024



### Code Transparency
- **Package**: `Azure.Security.CodeTransparency`
- **Service**: Confidential Ledger
- **Preview Version**: 1.0.0-beta.3
- **First Preview Date**: 03/26/2024

### Purview Data Map
- **Package**: `Azure.Analytics.Purview.DataMap`
- **Service**: Purview
- **Preview Version**: 1.0.0-beta.2
- **First Preview Date**: 03/12/2024



### OpenTelemetry LiveMetrics
- **Package**: `Azure.Monitor.OpenTelemetry.LiveMetrics`
- **Service**: Monitor
- **Preview Version**: 1.0.0-beta.3
- **First Preview Date**: 02/09/2024

### OpenAI Assistants
- **Package**: `Azure.AI.OpenAI.Assistants`
- **Service**: Cognitive Services
- **Preview Version**: 1.0.0-beta.4
- **First Preview Date**: 02/01/2024

## Summary

- **GA Packages**: 68 packages ready for implementation
- **Beta Packages (2024+)**: 10 packages for future consideration
- **Total Potential Tools**: 78 packages

## Notes

- This list excludes packages that are already implemented in Azure MCP Server
- This list focuses on **dataplane tools** and excludes most management plane packages (`Azure.ResourceManager.*`, `Azure.Provisioning.*`)
- GA packages should be prioritized as they have stable APIs
- Beta packages should be monitored for GA releases
- Some packages may be related or have dependencies that should be considered when implementing
- **Dataplane vs Management Plane**: Dataplane tools work with data and content (e.g., storage operations, database queries), while management plane tools handle resource lifecycle (create/delete resources)
