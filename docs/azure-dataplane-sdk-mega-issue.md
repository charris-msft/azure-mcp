# [ONBOARD] Dataplane SDK Mega-issue

The 68 tools shipped in [v0.3.1](https://github.com/Azure/azure-mcp/releases) have been excluded from the to-do list.

However, before starting on a tool please quickly double check for open issues and/or PRs that match the service name to avoid accidental duplicate work. When claiming an area, click the `...` and select `Convert to sub-issue` and then self-assign the sub-issue.

> ⚠️ If you are a Microsoft employee then please review our [Azure Internal Onboarding Documentation](https://aka.ms/azmcp/intake).

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
- [ ] **`Azure.AI.Agents.Persistent`** - Develop Agents using the Azure AI Agents Service (GA 1.0.0, 5/15/25)

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
- [x] **`Azure.Monitor.OpenTelemetry.Exporter`** - OpenTelemetry Exporter (GA 1.4.0, 05/09/2025)

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

## Priority 2: Beta Packages (Updated Since 2024)

These packages are in preview/beta but have been updated since 2024, indicating active development. Listed by priority:

### High Priority Beta (AI/Core Services)

- [ ] **`Azure.AI.OpenAI.Assistants`** - OpenAI Assistants (beta.4, 02/01/2024)
- [ ] **`Azure.AI.Inference`** - AI Model Inference (beta.5, 08/06/2024)
- [ ] **`Azure.AI.Language.Text`** - Language Text (beta.3, 08/08/2024)
- [ ] **`Azure.AI.Vision.Face`** - Face (beta.2, 05/27/2024)

---

## Proposed Azure MCP Tools and Suggested Prompts

Based on the dataplane SDKs listed above, the following tools should be created following the established naming convention `azmcp-<namespace>-<service>-<operation>`. 

### Storage Tools (Existing Namespace: `storage`)

#### Azure.Storage.Blobs.Batch
- `azmcp-storage-blob-batch-delete` - Delete multiple blobs in a single batch operation
- `azmcp-storage-blob-batch-set-tier` - Set access tier for multiple blobs in a batch operation
- `azmcp-storage-blob-batch-submit` - Submit a batch of blob operations

**Suggested Prompts:**
- "Delete a batch of old log files from my storage container"
- "Change the access tier to cool for multiple blobs in container <container-name>"
- "Submit a batch operation to delete blobs matching pattern log-2023-*"

#### Azure.Storage.Common
- `azmcp-storage-common-account-info` - Get common account information across storage services
- `azmcp-storage-common-credentials-validate` - Validate storage account credentials
- `azmcp-storage-common-endpoints-list` - List available storage service endpoints

**Suggested Prompts:**
- "Show me the common properties of my storage account <account-name>"
- "Validate my storage account credentials are working correctly"
- "List all available endpoints for storage account <account-name>"

#### Azure.Storage.Files.DataLake
- `azmcp-storage-files-datalake-filesystem-list` - List filesystems in a Data Lake Storage account
- `azmcp-storage-files-datalake-directory-create` - Create directories in Data Lake Storage
- `azmcp-storage-files-datalake-file-upload` - Upload files to Data Lake Storage
- `azmcp-storage-files-datalake-file-download` - Download files from Data Lake Storage
- `azmcp-storage-files-datalake-path-list` - List paths and directories in Data Lake Storage

**Suggested Prompts:**
- "List all filesystems in my Data Lake Storage account <account-name>"
- "Upload file <file-name> to Data Lake filesystem <filesystem-name>"
- "Create a new directory structure in Data Lake filesystem <filesystem-name>"
- "Download all files from Data Lake path /data/logs/"

#### Azure.Storage.Files.Shares
- `azmcp-storage-files-shares-list` - List file shares in a storage account
- `azmcp-storage-files-shares-create` - Create a new file share
- `azmcp-storage-files-shares-files-list` - List files in a file share
- `azmcp-storage-files-shares-file-upload` - Upload files to a file share
- `azmcp-storage-files-shares-file-download` - Download files from a file share

**Suggested Prompts:**
- "List all file shares in storage account <account-name>"
- "Create a new file share called backups with 100GB quota"
- "Upload local file <file-path> to file share <share-name>"
- "Download all files from file share <share-name> directory <directory>"

#### Azure.Storage.Queues
- `azmcp-storage-queue-message-send` - Send messages to a storage queue
- `azmcp-storage-queue-message-receive` - Receive messages from a storage queue
- `azmcp-storage-queue-message-peek` - Peek at messages without removing them
- `azmcp-storage-queue-properties-get` - Get queue properties and metadata

**Suggested Prompts:**
- "Send a message to queue <queue-name> with content <message>"
- "Receive the next message from queue <queue-name>"
- "Peek at the next 10 messages in queue <queue-name> without removing them"
- "Show me the properties and message count for queue <queue-name>"

#### Azure.Storage.DataMovement
- `azmcp-storage-datamovement-transfer-start` - Start a data transfer operation
- `azmcp-storage-datamovement-transfer-status` - Check status of data transfer operations
- `azmcp-storage-datamovement-transfer-pause` - Pause an ongoing data transfer
- `azmcp-storage-datamovement-transfer-resume` - Resume a paused data transfer

**Suggested Prompts:**
- "Start a bulk data transfer from <source> to <destination>"
- "Check the status of my current data transfer operations"
- "Pause the data transfer operation <transfer-id>"
- "Resume the paused data transfer operation <transfer-id>"

#### Azure.Storage.DataMovement.Blobs
- `azmcp-storage-datamovement-blob-copy` - Copy blobs using data movement optimizations
- `azmcp-storage-datamovement-blob-upload` - Upload large blobs with optimized data movement
- `azmcp-storage-datamovement-blob-download` - Download large blobs with optimized data movement

**Suggested Prompts:**
- "Copy all blobs from container <source-container> to <destination-container>"
- "Upload large file <file-path> to blob storage with optimal performance"
- "Download large blob <blob-name> from container <container-name> with resumable transfer"

#### Azure.Storage.DataMovement.Files.Shares
- `azmcp-storage-datamovement-files-copy` - Copy files between file shares with optimization
- `azmcp-storage-datamovement-files-upload` - Upload files to file shares with optimization
- `azmcp-storage-datamovement-files-download` - Download files from file shares with optimization

**Suggested Prompts:**
- "Copy files from file share <source-share> to <destination-share> with optimization"
- "Upload directory <local-directory> to file share <share-name> with optimal performance"
- "Download entire file share <share-name> to local directory <local-path>"

### AI Tools (New Namespace: `ai`) ⚠️

> **Note**: The `ai` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.AI.OpenAI
- `azmcp-ai-openai-completions-create` - Generate text completions using OpenAI models
- `azmcp-ai-openai-chat-completions-create` - Create chat completions
- `azmcp-ai-openai-embeddings-create` - Generate embeddings for text
- `azmcp-ai-openai-models-list` - List available OpenAI models

**Suggested Prompts:**
- "Generate a text completion for prompt: <prompt-text>"
- "Create a chat conversation about <topic> using GPT-4"
- "Generate embeddings for the text: <input-text>"
- "List all available OpenAI models in my Azure deployment"

#### Azure.AI.ContentSafety
- `azmcp-ai-contentsafety-text-analyze` - Analyze text content for safety issues
- `azmcp-ai-contentsafety-image-analyze` - Analyze images for safety issues
- `azmcp-ai-contentsafety-blocklist-manage` - Manage custom content blocklists

**Suggested Prompts:**
- "Analyze this text for content safety issues: <text-content>"
- "Check if this image contains harmful content: <image-url>"
- "Add term <term> to my custom content safety blocklist"

#### Azure.AI.TextAnalytics
- `azmcp-ai-textanalytics-sentiment-analyze` - Analyze sentiment in text
- `azmcp-ai-textanalytics-entities-extract` - Extract named entities from text
- `azmcp-ai-textanalytics-keyphrases-extract` - Extract key phrases from text
- `azmcp-ai-textanalytics-language-detect` - Detect language of text

**Suggested Prompts:**
- "Analyze the sentiment of this customer review: <review-text>"
- "Extract all named entities from this document: <document-text>"
- "Find the key phrases in this article: <article-text>"
- "Detect the language of this text: <foreign-text>"

#### Azure.AI.DocumentIntelligence
- `azmcp-ai-documentintelligence-analyze` - Analyze documents and extract structured data
- `azmcp-ai-documentintelligence-models-list` - List available document analysis models
- `azmcp-ai-documentintelligence-layout-analyze` - Extract layout information from documents

**Suggested Prompts:**
- "Extract all text and structure from this PDF document: <document-url>"
- "Analyze this invoice and extract key billing information"
- "Get the layout analysis of this scanned document including tables and forms"

#### Azure.AI.Translation.Text
- `azmcp-ai-translation-text-translate` - Translate text between languages
- `azmcp-ai-translation-text-languages-list` - List supported translation languages
- `azmcp-ai-translation-text-detect` - Detect the language of input text

**Suggested Prompts:**
- "Translate this text from English to Spanish: <text-to-translate>"
- "List all supported languages for text translation"
- "Detect the language of this text and translate it to English: <foreign-text>"

#### Azure.AI.Translation.Document
- `azmcp-ai-translation-document-translate` - Translate entire documents
- `azmcp-ai-translation-document-status` - Check document translation status
- `azmcp-ai-translation-document-formats-list` - List supported document formats

**Suggested Prompts:**
- "Translate this PDF document from French to English: <document-url>"
- "Check the translation status of document batch <batch-id>"
- "What document formats are supported for translation?"

#### Azure.AI.Vision.ImageAnalysis
- `azmcp-ai-vision-imageanalysis-analyze` - Analyze images and extract visual features
- `azmcp-ai-vision-imageanalysis-tags-extract` - Extract tags from images
- `azmcp-ai-vision-imageanalysis-objects-detect` - Detect objects in images
- `azmcp-ai-vision-imageanalysis-text-read` - Extract text from images (OCR)

**Suggested Prompts:**
- "Analyze this image and describe what you see: <image-url>"
- "Extract all text from this screenshot using OCR"
- "Detect and identify all objects in this photo: <photo-url>"
- "Generate descriptive tags for this product image"

### Communication Tools (New Namespace: `communication`) ⚠️

> **Note**: The `communication` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Communication.Email
- `azmcp-communication-email-send` - Send emails through Azure Communication Services
- `azmcp-communication-email-status-get` - Get the status of sent emails

**Suggested Prompts:**
- "Send an email to <recipient> with subject <subject> and message <message>"
- "Check the delivery status of email with ID <email-id>"

#### Azure.Communication.Chat
- `azmcp-communication-chat-thread-create` - Create a new chat thread
- `azmcp-communication-chat-message-send` - Send messages in a chat thread
- `azmcp-communication-chat-participants-list` - List participants in a chat thread

**Suggested Prompts:**
- "Create a new chat thread with participants <participant-list>"
- "Send message <message> to chat thread <thread-id>"
- "List all participants in chat thread <thread-id>"

#### Azure.Communication.Sms
- `azmcp-communication-sms-send` - Send SMS messages
- `azmcp-communication-sms-delivery-report` - Get SMS delivery reports

**Suggested Prompts:**
- "Send SMS message <message> to phone number <phone-number>"
- "Check the delivery status of SMS message <message-id>"

### Messaging Tools (New Namespace: `messaging`) ⚠️

> **Note**: The `messaging` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Messaging.EventGrid
- `azmcp-messaging-eventgrid-events-publish` - Publish events to Event Grid topics
- `azmcp-messaging-eventgrid-topics-list` - List Event Grid topics
- `azmcp-messaging-eventgrid-subscriptions-list` - List event subscriptions

**Suggested Prompts:**
- "Publish an event with data <event-data> to Event Grid topic <topic-name>"
- "List all Event Grid topics in my subscription"
- "Show me all event subscriptions for topic <topic-name>"

#### Azure.Messaging.EventHubs
- `azmcp-messaging-eventhubs-events-send` - Send events to Event Hubs
- `azmcp-messaging-eventhubs-events-receive` - Receive events from Event Hubs
- `azmcp-messaging-eventhubs-partitions-list` - List Event Hub partitions

**Suggested Prompts:**
- "Send event with payload <event-payload> to Event Hub <event-hub-name>"
- "Receive the latest events from Event Hub <event-hub-name>"
- "List all partitions for Event Hub <event-hub-name>"

### Container Tools (New Namespace: `containers`) ⚠️

> **Note**: The `containers` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Containers.ContainerRegistry
- `azmcp-containers-registry-repositories-list` - List repositories in a container registry
- `azmcp-containers-registry-tags-list` - List tags for a repository
- `azmcp-containers-registry-manifest-get` - Get manifest for a container image

**Suggested Prompts:**
- "List all repositories in container registry <registry-name>"
- "Show all tags for repository <repository-name> in registry <registry-name>"
- "Get the manifest for image <image-name>:<tag> in registry <registry-name>"

### Security Tools (Existing `keyvault` namespace + New `security` namespace) ⚠️

> **Note**: The `security` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Security.KeyVault.Certificates (extends existing `keyvault` namespace)
- `azmcp-keyvault-certificate-create` - Create a new certificate in Key Vault
- `azmcp-keyvault-certificate-import` - Import an existing certificate
- `azmcp-keyvault-certificate-renew` - Renew a certificate
- `azmcp-keyvault-certificate-export` - Export a certificate

**Suggested Prompts:**
- "Create a new self-signed certificate named <cert-name> in Key Vault <vault-name>"
- "Import certificate from file <cert-file> into Key Vault <vault-name>"
- "Renew the certificate <cert-name> in Key Vault <vault-name>"

#### Azure.Security.KeyVault.Administration (extends existing `keyvault` namespace)
- `azmcp-keyvault-admin-backup-create` - Create a full Key Vault backup
- `azmcp-keyvault-admin-backup-restore` - Restore Key Vault from backup
- `azmcp-keyvault-admin-settings-get` - Get Key Vault administration settings

**Suggested Prompts:**
- "Create a full backup of Key Vault <vault-name>"
- "Restore Key Vault <vault-name> from backup <backup-location>"
- "Show me the administration settings for Key Vault <vault-name>"

#### Azure.Security.ConfidentialLedger (new `security` namespace)
- `azmcp-security-confidentialledger-entries-append` - Append entries to the ledger
- `azmcp-security-confidentialledger-entries-get` - Retrieve entries from the ledger
- `azmcp-security-confidentialledger-receipt-get` - Get transaction receipts

**Suggested Prompts:**
- "Add a new entry with data <entry-data> to Confidential Ledger <ledger-name>"
- "Retrieve entries from Confidential Ledger <ledger-name> for transaction <transaction-id>"
- "Get the receipt for transaction <transaction-id> in ledger <ledger-name>"

### Monitor Tools (Existing Namespace: `monitor`)

#### Azure.Monitor.Ingestion
- `azmcp-monitor-ingestion-logs-upload` - Upload custom logs to Azure Monitor
- `azmcp-monitor-ingestion-data-validate` - Validate log data before ingestion
- `azmcp-monitor-ingestion-status-check` - Check the status of log ingestion

**Suggested Prompts:**
- "Upload custom log data <log-data> to Azure Monitor workspace <workspace-name>"
- "Validate this log data before sending to Azure Monitor: <log-data>"
- "Check the ingestion status for data collection rule <rule-name>"

### Data Tools (New Namespace: `data`) ⚠️

> **Note**: The `data` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Data.SchemaRegistry
- `azmcp-data-schemaregistry-schemas-list` - List schemas in the registry
- `azmcp-data-schemaregistry-schema-register` - Register a new schema
- `azmcp-data-schemaregistry-schema-get` - Get a specific schema
- `azmcp-data-schemaregistry-schema-validate` - Validate data against a schema

**Suggested Prompts:**
- "List all schemas in Schema Registry <registry-name>"
- "Register a new schema named <schema-name> with definition <schema-definition>"
- "Get the schema definition for schema <schema-name>"
- "Validate this JSON data against schema <schema-name>: <json-data>"

### Health Tools (New Namespace: `health`) ⚠️

> **Note**: The `health` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Health.Deidentification
- `azmcp-health-deidentification-text-process` - De-identify text containing health information
- `azmcp-health-deidentification-job-submit` - Submit a de-identification job
- `azmcp-health-deidentification-job-status` - Check status of de-identification jobs

**Suggested Prompts:**
- "De-identify this medical text: <medical-text>"
- "Submit a batch de-identification job for documents in <source-location>"
- "Check the status of de-identification job <job-id>"

### IoT Tools (New Namespace: `iot`) ⚠️

> **Note**: The `iot` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.DigitalTwins.Core
- `azmcp-iot-digitaltwins-models-list` - List digital twin models
- `azmcp-iot-digitaltwins-instances-create` - Create digital twin instances
- `azmcp-iot-digitaltwins-instances-query` - Query digital twin instances
- `azmcp-iot-digitaltwins-relationships-create` - Create relationships between twins

**Suggested Prompts:**
- "List all digital twin models in instance <instance-name>"
- "Create a new digital twin of type <model-type> with ID <twin-id>"
- "Query for all digital twins with property <property-name> = <value>"
- "Create a relationship between twin <twin1-id> and twin <twin2-id>"

#### Azure.IoT.DeviceUpdate
- `azmcp-iot-deviceupdate-updates-list` - List available device updates
- `azmcp-iot-deviceupdate-deployment-create` - Create device update deployments
- `azmcp-iot-deviceupdate-deployment-status` - Check deployment status

**Suggested Prompts:**
- "List all available updates for device type <device-type>"
- "Deploy update <update-id> to device group <device-group>"
- "Check the status of deployment <deployment-id>"

### Mixed Reality Tools (New Namespace: `mixedreality`) ⚠️

> **Note**: The `mixedreality` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.MixedReality.RemoteRendering
- `azmcp-mixedreality-remoterendering-session-create` - Create remote rendering sessions
- `azmcp-mixedreality-remoterendering-session-status` - Check session status
- `azmcp-mixedreality-remoterendering-models-convert` - Convert 3D models for remote rendering

**Suggested Prompts:**
- "Create a new remote rendering session with size <session-size>"
- "Check the status of remote rendering session <session-id>"
- "Convert 3D model <model-url> for remote rendering"

### DevOps Tools (New Namespace: `devops`) ⚠️

> **Note**: The `devops` namespace does not exist yet in the current Azure MCP server implementation.

#### Azure.Developer.LoadTesting
- `azmcp-devops-loadtesting-tests-create` - Create load tests
- `azmcp-devops-loadtesting-tests-run` - Run load tests
- `azmcp-devops-loadtesting-results-get` - Get load test results

**Suggested Prompts:**
- "Create a load test for URL <target-url> with <concurrent-users> concurrent users"
- "Run the load test <test-name> and show me the results"
- "Get the detailed results for load test run <run-id>"

#### Azure.Developer.DevCenter
- `azmcp-devops-devcenter-environments-list` - List development environments
- `azmcp-devops-devcenter-environment-create` - Create development environments
- `azmcp-devops-devcenter-projects-list` - List Dev Center projects

**Suggested Prompts:**
- "List all available development environments in Dev Center <devcenter-name>"
- "Create a new development environment using template <template-name>"
- "Show me all projects in Dev Center <devcenter-name>"

---

## Summary

This document provides a comprehensive overview of all Azure dataplane SDKs that need to be onboarded as MCP tools, along with proposed tool names following the established naming convention and realistic suggested prompts based on common use cases for each service.

**New Namespaces Required:**
- `ai` - For AI and Cognitive Services tools
- `communication` - For Azure Communication Services tools  
- `messaging` - For Event Grid, Event Hubs, and messaging tools
- `containers` - For Container Registry tools
- `security` - For security services beyond Key Vault
- `data` - For data services like Schema Registry
- `health` - For healthcare-specific AI services
- `iot` - For IoT and Digital Twins services
- `mixedreality` - For Mixed Reality services
- `devops` - For developer and DevOps tools

**Existing Namespaces Extended:**
- `storage` - Extended with new blob batch, data movement, files, and queue operations
- `keyvault` - Extended with certificate and administration operations
- `monitor` - Extended with ingestion capabilities

Fixes #557.