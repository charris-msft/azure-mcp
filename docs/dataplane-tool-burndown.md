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

These packages have stable GA releases and are sorted by popularity/usage:

### Core Infrastructure Services (Highest Priority)

#### Storage Services
- [ ] **`Azure.Storage.Blobs.Batch`** - Storage Blobs Batch (GA 12.21.0, 03/11/2025)
- [ ] **`Azure.Storage.Common`** - Storage Common (GA 12.23.0, 03/11/2025)
- [ ] **`Azure.Storage.Files.DataLake`** - Storage Files Data Lake (GA 12.22.0, 03/11/2025)
- [ ] **`Azure.Storage.Files.Shares`** - Storage Files Share (GA 12.22.0, 03/11/2025)
- [ ] **`Azure.Storage.Queues`** - Storage Queues (GA 12.22.0, 03/11/2025)
- [ ] **`Azure.Storage.DataMovement`** - Data Movement (GA 12.1.0, 02/27/2025)
- [ ] **`Azure.Storage.DataMovement.Blobs`** - Data Movement Blobs (GA 12.1.0, 02/27/2025)
- [ ] **`Azure.Storage.DataMovement.Files.Shares`** - Data Movement Files Shares (GA 12.1.0, 02/27/2025)

#### AI/Cognitive Services (High Demand)
- [ ] **`Azure.AI.OpenAI`** - OpenAI Inference (GA 2.1.0, 12/06/2024)
- [ ] **`Azure.AI.ContentSafety`** - Content Safety (GA 1.0.0, 12/12/2023)
- [ ] **`Azure.AI.TextAnalytics`** - Text Analytics (GA 5.3.0, 06/19/2023)
- [ ] **`Azure.AI.DocumentIntelligence`** - Document Intelligence (GA 1.0.0, 12/17/2024)
- [ ] **`Azure.AI.Translation.Text`** - Text Translation (GA 1.0.0, 05/20/2024)
- [ ] **`Azure.AI.Translation.Document`** - Document Translation (GA 2.0.0, 11/13/2024)
- [ ] **`Azure.AI.Vision.ImageAnalysis`** - Image Analysis (GA 1.0.0, 10/24/2024)
- [ ] **`Azure.AI.FormRecognizer`** - Form Recognizer (GA 4.1.0, 08/10/2023)
- [ ] **`Azure.AI.Language.Conversations`** - Conversational Language Understanding (GA 1.1.0, 06/14/2023)
- [ ] **`Azure.AI.Language.QuestionAnswering`** - Question Answering (GA 1.1.0, 10/13/2022)

#### Communication Services
- [ ] **`Azure.Communication.Email`** - Communication Email (GA 1.0.1, 08/18/2023)
- [ ] **`Azure.Communication.Chat`** - Communication Chat (GA 1.4.0, 06/24/2025)
- [ ] **`Azure.Communication.Sms`** - Communication SMS (GA 1.0.1, 05/25/2021)
- [ ] **`Azure.Communication.Common`** - Communication Common (GA 1.4.0, 06/04/2025)
- [ ] **`Azure.Communication.Identity`** - Communication Identity (GA 1.3.1, 03/22/2024)
- [ ] **`Azure.Communication.PhoneNumbers`** - Communication Phone Numbers (GA 1.4.0, 06/20/2025)
- [ ] **`Azure.Communication.CallAutomation`** - Communication Call Automation (GA 1.4.0, 06/04/2025)
- [ ] **`Azure.Communication.Messages`** - Communication Messages (GA 1.1.0, 10/18/2024)
- [ ] **`Azure.Communication.Rooms`** - Communication Rooms (GA 1.2.0, 03/17/2025)

#### Event & Messaging Services
- [ ] **`Azure.Messaging.EventGrid`** - Event Grid (GA 5.0.0, 06/26/2025)
- [ ] **`Azure.Messaging.EventHubs`** - Event Hubs (GA 5.12.2, 06/12/2025)
- [ ] **`Azure.Messaging.EventHubs.Processor`** - Event Hubs Event Processor (GA 5.12.2, 06/13/2025)
- [ ] **`Azure.Messaging.WebPubSub`** - Web PubSub (GA 1.6.0, 05/08/2025)
- [ ] **`Azure.Messaging.WebPubSub.Client`** - Web PubSub Client (GA 1.0.0, 12/31/2023)
- [ ] **`Azure.Messaging.EventGrid.SystemEvents`** - Event Grid System Events (GA 1.0.0, 06/25/2025)
- [ ] **`Azure.Messaging.EventGrid.Namespaces`** - Event Grid Namespaces (GA 1.1.0, 05/12/2025)

### High Priority Services

#### Container & DevOps
- [ ] **`Azure.Containers.ContainerRegistry`** - Container Registry (GA 1.2.0, 02/07/2025)
- [ ] **`Azure.Developer.LoadTesting`** - Load Testing (GA 1.0.2, 01/20/2025)
- [ ] **`Azure.Developer.DevCenter`** - Dev Center (GA 1.0.0, 04/03/2024)

#### Security & Key Management
- [ ] **`Azure.Security.KeyVault.Certificates`** - Key Vault Certificates (GA 4.8.0, 06/17/2025)
- [ ] **`Azure.Security.KeyVault.Administration`** - Key Vault Administration (GA 4.6.0, 06/18/2025)
- [ ] **`Azure.Security.ConfidentialLedger`** - Confidential Ledger (GA 1.3.0, 02/05/2024)
- [ ] **`Azure.Security.Attestation`** - Attestation (GA 1.0.0, 05/07/2021)

#### Monitoring & Observability
- [ ] **`Azure.Monitor.Ingestion`** - Monitor Ingestion (GA 1.1.2, 04/03/2024)
- [ ] **`Azure.Monitor.OpenTelemetry.Exporter`** - OpenTelemetry Exporter (GA 1.4.0, 05/09/2025)

#### Data & Schema
- [ ] **`Azure.Data.SchemaRegistry`** - Schema Registry (GA 1.4.0, 09/18/2024)

### Medium Priority Services

#### Health Services
- [ ] **`Azure.Health.Deidentification`** - Health Deidentification (GA 1.0.0, 04/23/2025)
- [ ] **`Azure.Health.Insights.RadiologyInsights`** - Health Insights Radiology Insights (GA 1.1.0, 06/13/2025)

#### IoT Services
- [ ] **`Azure.DigitalTwins.Core`** - Digital Twins (GA 1.6.0, 06/04/2025)
- [ ] **`Azure.IoT.DeviceUpdate`** - Device Update (GA 1.0.0, 09/08/2022)

#### Specialized AI Services
- [ ] **`Azure.AI.Agents.Persistent`** - AI Agents Persistent (GA 1.0.0, 05/15/2025)
- [ ] **`Azure.AI.ContentSafety.Extension.Embedded.Text`** - Content Safety Extension Embedded Text (GA 1.0.0)
- [ ] **`Azure.AI.MetricsAdvisor`** - Metrics Advisor (GA 1.1.0, 08/10/2021)

### Lower Priority / Specialized Services

#### Mixed Reality
- [ ] **`Azure.MixedReality.RemoteRendering`** - Azure Remote Rendering (GA 1.1.0, 09/17/2021)
- [ ] **`Azure.MixedReality.Authentication`** - Mixed Reality Authentication (GA 1.2.0, 09/09/2022)

#### .NET Extensions & Integration
- [ ] **`Azure.Extensions.AspNetCore.DataProtection.Blobs`** - Blob Storage Key Store for .NET Data Protection (GA 1.5.1, 06/23/2025)
- [ ] **`Azure.Extensions.AspNetCore.DataProtection.Keys`** - Key Encryptor for .NET Data Protection (GA 1.6.1, 06/23/2025)
- [ ] **`Azure.Extensions.AspNetCore.Configuration.Secrets`** - Secrets Configuration Provider for .NET (GA 1.4.0, 02/07/2025)

#### Platform & Resource Management
- [ ] **`Azure.Provisioning`** - Provisioning (GA 1.2.0, 07/08/2025)
- [ ] **`Azure.ResourceManager`** - Resource Manager (GA 1.13.1, 04/24/2025)
- [ ] **`Azure.ResourceManager.EventGrid`** - Resource Management Event Grid (GA 1.1.0, 03/31/2025)
- [ ] **`Azure.ResourceManager.EventHubs`** - Resource Management Event Hubs (GA 1.2.0, 06/13/2025)
- [ ] **`Azure.ResourceManager.KeyVault`** - Resource Management Key Vault (GA 1.3.2, 04/02/2025)
- [ ] **`Azure.ResourceManager.ServiceBus`** - Resource Management Service Bus (GA 1.1.0, 11/28/2024)
- [ ] **`Azure.ResourceManager.WebPubSub`** - Resource Management Web PubSub (GA 1.2.0, 06/13/2025)

#### Legacy/Deprecated Services
- [ ] **`Azure.Batch.FileStaging`** - Azure Batch File Staging (GA 8.0.1)
- [ ] **`Azure.Communication.Calling.WindowsClient`** - Communication Calling Windows Client (GA 1.11.1)
- [ ] **`Azure.Communication.JobRouter`** - Communication JobRouter (GA 1.0.0, 11/17/2023)
- [ ] **`Azure.Communication.NetworkTraversal`** - Communication Network Traversal (GA 1.0.0, 02/09/2022) *deprecated*
- [ ] **`Azure.DCAP.Windows`** - DCAP Windows (GA 1.2.0)

## Priority 2: Beta Packages (Updated Since 2024)

These packages are in preview/beta but have been updated since 2024, indicating active development. Listed by priority:

### High Priority Beta (AI/Core Services)
- [ ] **`Azure.AI.OpenAI.Assistants`** - OpenAI Assistants (beta.4, 02/01/2024)
- [ ] **`Azure.AI.Inference`** - AI Model Inference (beta.5, 08/06/2024)
- [ ] **`Azure.AI.Language.Text`** - Language Text (beta.3, 08/08/2024)
- [ ] **`Azure.AI.Vision.Face`** - Face (beta.2, 05/27/2024)

### Medium Priority Beta (Infrastructure & Tools)
- [ ] **`Azure.Compute.Batch`** - Compute Batch (beta.3, 07/02/2024)
- [ ] **`Azure.Monitor.OpenTelemetry.LiveMetrics`** - OpenTelemetry LiveMetrics (beta.3, 02/09/2024)
- [ ] **`Azure.Analytics.Purview.DataMap`** - Purview Data Map (beta.2, 03/12/2024)

### Lower Priority Beta (Testing & Security)
- [ ] **`Azure.Developer.MicrosoftPlaywrightTesting.TestLogger`** - Microsoft Playwright Testing TestLogger (beta.4, 10/23/2024)
- [ ] **`Azure.Developer.MicrosoftPlaywrightTesting.NUnit`** - Microsoft Playwright Testing NUnit (beta.4, 10/23/2024)
- [ ] **`Azure.Security.CodeTransparency`** - Code Transparency (beta.3, 03/26/2024)

## Summary

- **GA Packages**: 68 packages ready for implementation (all now with checkboxes for tracking)
- **Beta Packages (2024+)**: 10 packages for future consideration (all now with checkboxes for tracking)
- **Total Potential Tools**: 78 packages

### Prioritization Approach

The packages are now organized by priority based on:

1. **Core Infrastructure Services** (Storage, AI, Communication, Messaging) - Highest priority due to broad usage
2. **High Priority Services** (Container Registry, Security, Monitoring) - Commonly used in enterprise scenarios  
3. **Medium Priority Services** (Health, IoT, Specialized AI) - Important for specific use cases
4. **Lower Priority / Specialized Services** - Niche or platform-specific functionality

All items are formatted as unchecked checkboxes (`- [ ]`) for easy copy-paste into GitHub issues for project tracking.

## Notes

- This list excludes packages that are already implemented in Azure MCP Server
- This list focuses on **dataplane tools** and excludes most management plane packages (`Azure.ResourceManager.*`, `Azure.Provisioning.*`)
- GA packages should be prioritized as they have stable APIs
- Beta packages should be monitored for GA releases
- Some packages may be related or have dependencies that should be considered when implementing
- **Dataplane vs Management Plane**: Dataplane tools work with data and content (e.g., storage operations, database queries), while management plane tools handle resource lifecycle (create/delete resources)
