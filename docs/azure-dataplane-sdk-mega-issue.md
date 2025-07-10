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

### Storage Core Tools (Existing Namespace: `storage`) - 10 tools

- [ ] #### Azure.Storage.Blobs.Batch
- `azmcp-storage-blob-batch-delete` - Delete multiple blobs in a single batch operation. This tool allows efficient deletion of many blobs at once using Azure's batch API, reducing API calls and improving performance. Requires account-name, container-name, and blob patterns or names.
- `azmcp-storage-blob-batch-set-tier` - Set access tier for multiple blobs in a single batch operation. This tool efficiently changes the storage tier (Hot, Cool, Archive) for multiple blobs simultaneously, optimizing storage costs. Requires account-name, container-name, tier-name, and blob patterns.
- `azmcp-storage-blob-batch-submit` - Submit a batch of blob operations for execution. This tool allows you to compose and execute multiple blob operations (delete, set tier, etc.) in a single atomic batch request. Requires account-name and a collection of batch operations.

**Suggested Prompts:**
- "Delete a batch of old log files from my storage container"
- "Change the access tier to cool for multiple blobs in container <container-name>"
- "Submit a batch operation to delete blobs matching pattern log-2023-*"

- [ ] #### Azure.Storage.Common
- `azmcp-storage-common-account-info` - Get common account information across storage services. This tool retrieves shared metadata and properties that apply to all storage services (blobs, files, queues, tables) within a storage account. Returns account details as JSON including creation date, replication type, and service availability.
- `azmcp-storage-common-credentials-validate` - Validate storage account credentials and connectivity. This tool verifies that the provided credentials can successfully authenticate with Azure Storage services and tests basic connectivity. Returns validation status and any authentication errors.
- `azmcp-storage-common-endpoints-list` - List available storage service endpoints for an account. This tool retrieves all primary and secondary endpoints for blob, file, queue, and table services within a storage account. Returns endpoint URLs as a JSON array.

**Suggested Prompts:**
- "Show me the common properties of my storage account <account-name>"
- "Validate my storage account credentials are working correctly"
- "List all available endpoints for storage account <account-name>"

- [ ] #### Azure.Storage.Files.DataLake
- `azmcp-storage-datalake-filesystem-list` - List all filesystems in a Data Lake Storage account. This tool retrieves all filesystems (containers) available in the specified Data Lake Storage Gen2 account. Returns filesystem names, creation dates, and metadata as a JSON array. Requires account-name.
- `azmcp-storage-datalake-directory-create` - Create directories and nested directory structures in Data Lake Storage. This tool allows creation of single directories or complex nested directory hierarchies within a Data Lake filesystem. Requires account-name, filesystem-name, and directory-path.
- `azmcp-storage-datalake-file-upload` - Upload files to Data Lake Storage with hierarchical namespace support. This tool uploads local files or content to specific paths within a Data Lake filesystem, preserving directory structures. Requires account-name, filesystem-name, file-path, and source content.
- `azmcp-storage-datalake-file-download` - Download files from Data Lake Storage to local storage or retrieve content. This tool downloads files from specific paths within a Data Lake filesystem and returns the content or saves locally. Requires account-name, filesystem-name, and file-path.
- `azmcp-storage-datalake-path-list` - List all paths, directories, and files in a Data Lake Storage filesystem. This tool recursively lists all items within a specified path, including subdirectories and files with their metadata. Returns path information as JSON. Requires account-name, filesystem-name, and optional path prefix.

**Suggested Prompts:**
- "List all filesystems in my Data Lake Storage account <account-name>"
- "Upload file <file-name> to Data Lake filesystem <filesystem-name>"
- "Create a new directory structure in Data Lake filesystem <filesystem-name>"
- "Download all files from Data Lake path /data/logs/"

### Storage Files Tools (New Namespace: `storage-files`) - 5 tools

- [ ] #### Azure.Storage.Files.Shares
- `azmcp-storage-files-shares-list` - List all file shares in a storage account. This tool retrieves all Azure Files shares within the specified storage account, including share metadata, quotas, and usage statistics. Returns share information as a JSON array. Requires account-name.
- `azmcp-storage-files-shares-create` - Create a new file share with specified quota and metadata. This tool creates new Azure Files shares with configurable storage quotas and custom metadata properties. Requires account-name, share-name, and optional quota settings.
- `azmcp-storage-files-shares-files-list` - List all files and directories within a file share. This tool recursively lists all items in a specified file share directory, including files, subdirectories, and their properties. Returns file listing as JSON. Requires account-name, share-name, and optional directory-path.
- `azmcp-storage-files-shares-file-upload` - Upload files to an Azure Files share directory. This tool uploads local files or content to specific paths within an Azure Files share, creating necessary parent directories. Requires account-name, share-name, file-path, and source content.
- `azmcp-storage-files-shares-file-download` - Download files from an Azure Files share to local storage. This tool downloads files from specific paths within an Azure Files share and returns the content or saves locally. Requires account-name, share-name, and file-path.

**Suggested Prompts:**
- "List all file shares in storage account <account-name>"
- "Create a new file share called backups with 100GB quota"
- "Upload local file <file-path> to file share <share-name>"
- "Download all files from file share <share-name> directory <directory>"

- [ ] #### Azure.Storage.Queues
- `azmcp-storage-queue-message-send` - Send messages to an Azure Storage queue for asynchronous processing. This tool adds messages to a specified queue with configurable time-to-live and visibility delay settings. Messages are returned with receipt handles for tracking. Requires account-name, queue-name, and message-content.
- `azmcp-storage-queue-message-receive` - Receive and optionally delete messages from an Azure Storage queue. This tool retrieves messages from the queue front, making them invisible to other consumers for a specified duration. Returns message content and receipt handles as JSON. Requires account-name and queue-name.
- `azmcp-storage-queue-message-peek` - Peek at queue messages without removing them from the queue. This tool allows inspection of upcoming messages without affecting their visibility or position in the queue. Returns message content and metadata as JSON without receipt handles. Requires account-name and queue-name.
- `azmcp-storage-queue-properties-get` - Get queue properties, metadata, and message statistics. This tool retrieves queue configuration including message count, metadata properties, and approximate size information. Returns queue properties as JSON. Requires account-name and queue-name.

**Suggested Prompts:**
- "Send a message to queue <queue-name> with content <message>"
- "Receive the next message from queue <queue-name>"
- "Peek at the next 10 messages in queue <queue-name> without removing them"
- "Show me the properties and message count for queue <queue-name>"

### Storage Data Movement Tools (New Namespace: `storage-datamovement`) - 7 tools

- [ ] #### Azure.Storage.DataMovement
- `azmcp-storage-datamovement-transfer-status` - Check the status and progress of active data transfer operations. This tool provides real-time status updates including bytes transferred, transfer rate, and completion percentage for ongoing operations. Returns transfer status as JSON. Requires transfer-id.
- `azmcp-storage-datamovement-transfer-pause` - Pause an active data transfer operation to temporarily halt progress. This tool suspends ongoing transfers while preserving state for later resumption, useful for managing bandwidth or system resources. Returns pause confirmation. Requires transfer-id.
- `azmcp-storage-datamovement-transfer-resume` - Resume a previously paused data transfer operation from its last checkpoint. This tool restarts suspended transfers from their last successful state, continuing the operation without data loss. Returns resume confirmation. Requires transfer-id.

**Suggested Prompts:**
- "Start a bulk data transfer from <source> to <destination>"
- "Check the status of my current data transfer operations"
- "Pause the data transfer operation <transfer-id>"
- "Resume the paused data transfer operation <transfer-id>"

- [ ] #### Azure.Storage.DataMovement.Blobs
- `azmcp-storage-datamovement-blob-copy` - Copy blobs between containers or accounts using optimized data movement. This tool performs high-performance blob copying with automatic chunking, parallel transfers, and retry logic for large-scale data movement. Returns copy operation status. Requires source-blob-url, destination-blob-url.
- `azmcp-storage-datamovement-blob-upload` - Upload large files to blob storage using optimized data movement with resumable transfers. This tool handles large file uploads with automatic chunking, parallel uploads, and checkpoint-based resumption for reliability. Returns upload operation status. Requires account-name, container-name, blob-name, and source-file-path.
- `azmcp-storage-datamovement-blob-download` - Download large blobs from storage using optimized data movement with resumable transfers. This tool performs high-performance downloads with automatic chunking, parallel streams, and resumption capabilities for large blobs. Returns download operation status. Requires account-name, container-name, blob-name, and destination-file-path.

**Suggested Prompts:**
- "Copy all blobs from container <source-container> to <destination-container>"
- "Upload large file <file-path> to blob storage with optimal performance"
- "Download large blob <blob-name> from container <container-name> with resumable transfer"

- [ ] #### Azure.Storage.DataMovement.Files.Shares
- `azmcp-storage-datamovement-files-copy` - Copy files between Azure Files shares using optimized data movement with parallel transfers. This tool performs high-performance file copying across shares or storage accounts with automatic retry logic and progress tracking. Returns copy operation status. Requires source-file-url, destination-file-url.
- `azmcp-storage-datamovement-files-upload` - Upload files and directories to Azure Files shares using optimized data movement. This tool handles large file and directory uploads with parallel transfers, automatic chunking, and resumable operation support. Returns upload operation status. Requires account-name, share-name, file-path, and source-content.
- `azmcp-storage-datamovement-files-download` - Download files and directories from Azure Files shares using optimized data movement. This tool performs high-performance downloads with parallel streams and resumable transfers for large files and directory structures. Returns download operation status. Requires account-name, share-name, file-path, and destination-path.

**Suggested Prompts:**
- "Copy files from file share <source-share> to <destination-share> with optimization"
- "Upload directory <local-directory> to file share <share-name> with optimal performance"
- "Download entire file share <share-name> to local directory <local-path>"

### AI Core Tools (New Namespace: `ai`) - 14 tools ⚠️

> **Note**: The `ai` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.AI.OpenAI
- `azmcp-ai-openai-completions-create` - Generate text completions using deployed Azure OpenAI models. This tool sends prompts to Azure OpenAI completion models and returns generated text with configurable parameters like temperature and max tokens. Returns completion text as JSON. Requires resource-name, deployment-name, and prompt-text.
- `azmcp-ai-openai-chat-completions-create` - Create interactive chat completions using Azure OpenAI chat models. This tool processes conversational inputs with message history and system instructions to generate contextual responses. Returns chat response as JSON. Requires resource-name, deployment-name, and message-array.
- `azmcp-ai-openai-embeddings-create` - Generate vector embeddings for text using Azure OpenAI embedding models. This tool converts text into high-dimensional vector representations for similarity search and machine learning applications. Returns embedding vectors as JSON array. Requires resource-name, deployment-name, and input-text.
- `azmcp-ai-openai-models-list` - List all available OpenAI models and deployments in an Azure resource. This tool retrieves information about deployed models including model names, versions, capabilities, and deployment status. Returns model information as JSON array. Requires resource-name.

**Suggested Prompts:**
- "Generate a text completion for prompt: <prompt-text>"
- "Create a chat conversation about <topic> using GPT-4"
- "Generate embeddings for the text: <input-text>"
- "List all available OpenAI models in my Azure deployment"

- [ ] #### Azure.AI.ContentSafety
- `azmcp-ai-contentsafety-text-analyze` - Analyze text content for harmful material including hate speech, violence, and sexual content. This tool scans text for policy violations and returns severity scores across multiple safety categories with detailed classification results. Returns safety analysis as JSON. Requires resource-name and text-content.
- `azmcp-ai-contentsafety-image-analyze` - Analyze images for harmful visual content including violence, self-harm, and inappropriate material. This tool processes images and returns safety classification scores across multiple categories with confidence levels. Returns image safety analysis as JSON. Requires resource-name and image-url or image-data.
- `azmcp-ai-contentsafety-blocklist-manage` - Create and manage custom content blocklists for enhanced content filtering. This tool allows creation, modification, and deletion of custom term blocklists to supplement built-in content safety policies. Returns blocklist operation status. Requires resource-name, operation-type, and blocklist-terms.

**Suggested Prompts:**
- "Analyze this text for content safety issues: <text-content>"
- "Check if this image contains harmful content: <image-url>"
- "Add term <term> to my custom content safety blocklist"

- [ ] #### Azure.AI.TextAnalytics
- `azmcp-ai-textanalytics-sentiment-analyze` - Analyze sentiment and emotional tone in text content with confidence scores. This tool processes text to determine positive, negative, or neutral sentiment with detailed confidence metrics and opinion mining capabilities. Returns sentiment analysis as JSON. Requires resource-name and text-content.
- `azmcp-ai-textanalytics-entities-extract` - Extract and classify named entities from text including people, locations, organizations, and custom entities. This tool identifies and categorizes entities with confidence scores and linking to knowledge bases where applicable. Returns entity extraction results as JSON. Requires resource-name and text-content.
- `azmcp-ai-textanalytics-keyphrases-extract` - Extract key phrases and important terms from text content for summarization and indexing. This tool identifies the most relevant phrases and concepts within text for content analysis and search optimization. Returns key phrases as JSON array. Requires resource-name and text-content.
- `azmcp-ai-textanalytics-language-detect` - Detect the primary language of text content with confidence scoring. This tool identifies the language of input text from over 120 supported languages and returns ISO language codes with confidence levels. Returns language detection results as JSON. Requires resource-name and text-content.

**Suggested Prompts:**
- "Analyze the sentiment of this customer review: <review-text>"
- "Extract all named entities from this document: <document-text>"
- "Find the key phrases in this article: <article-text>"
- "Detect the language of this text: <foreign-text>"

### AI Vision & Document Tools (New Namespace: `ai-vision`) - 10 tools ⚠️

> **Note**: The `ai-vision` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.AI.DocumentIntelligence
- `azmcp-ai-vision-documentintelligence-analyze` - Analyze documents and extract structured data including text, tables, key-value pairs, and custom fields. This tool processes various document types (PDFs, images, Office docs) using pre-built or custom models to extract structured information. Returns document analysis results as JSON. Requires resource-name, document-url, and optional model-id.
- `azmcp-ai-vision-documentintelligence-models-list` - List all available document analysis models including pre-built and custom trained models. This tool retrieves model information including supported document types, field schemas, and model capabilities. Returns model details as JSON array. Requires resource-name.
- `azmcp-ai-vision-documentintelligence-layout-analyze` - Extract layout information from documents including text regions, tables, selection marks, and reading order. This tool analyzes document structure and formatting without applying specific business logic, useful for general document processing. Returns layout analysis as JSON. Requires resource-name and document-url.

**Suggested Prompts:**
- "Extract all text and structure from this PDF document: <document-url>"
- "Analyze this invoice and extract key billing information"
- "Get the layout analysis of this scanned document including tables and forms"

- [ ] #### Azure.AI.Translation.Text
- `azmcp-ai-vision-translation-text-translate` - Translate text content between supported languages with quality optimization. This tool performs high-quality text translation using neural machine translation models with support for multiple target languages and custom dictionaries. Returns translated text as JSON. Requires resource-name, source-text, target-language.
- `azmcp-ai-vision-translation-text-languages-list` - List all supported languages for text translation with language codes and names. This tool retrieves available source and target languages including regional variants and script options for translation services. Returns language information as JSON array. Requires resource-name.
- `azmcp-ai-vision-translation-text-detect` - Detect the language of input text with confidence scoring and script identification. This tool identifies the source language of text content from over 100 supported languages and returns ISO language codes with confidence levels. Returns language detection results as JSON. Requires resource-name and text-content.

**Suggested Prompts:**
- "Translate this text from English to Spanish: <text-to-translate>"
- "List all supported languages for text translation"
- "Detect the language of this text and translate it to English: <foreign-text>"

- [ ] #### Azure.AI.Translation.Document
- `azmcp-ai-vision-translation-document-translate` - Translate entire documents while preserving formatting and structure. This tool processes complete documents (PDF, Word, PowerPoint, etc.) and returns translated versions maintaining original layout, fonts, and formatting. Returns translation operation status and output document URLs. Requires resource-name, source-document-url, target-language.
- `azmcp-ai-vision-translation-document-status` - Check the status and progress of document translation operations. This tool monitors ongoing translation jobs providing completion percentage, processing time estimates, and error information for batch document operations. Returns operation status as JSON. Requires resource-name and operation-id.
- `azmcp-ai-vision-translation-document-formats-list` - List all supported document formats for translation including file types and size limits. This tool retrieves format specifications including maximum file sizes, supported extensions, and format-specific limitations. Returns format information as JSON array. Requires resource-name.

**Suggested Prompts:**
- "Translate this PDF document from French to English: <document-url>"
- "Check the translation status of document batch <batch-id>"
- "What document formats are supported for translation?"

- [ ] #### Azure.AI.Vision.ImageAnalysis
- `azmcp-ai-vision-imageanalysis-analyze` - Analyze images and extract comprehensive visual features including objects, text, faces, and scene descriptions. This tool performs multi-modal image analysis returning detailed visual information, captions, and confidence scores for various detected elements. Returns analysis results as JSON. Requires resource-name and image-url or image-data.
- `azmcp-ai-vision-imageanalysis-tags-extract` - Extract descriptive tags and labels from images with confidence scoring. This tool identifies visual elements, objects, activities, and concepts within images to generate relevant tags for categorization and search. Returns tag array as JSON. Requires resource-name and image-url or image-data.
- `azmcp-ai-vision-imageanalysis-objects-detect` - Detect and locate objects within images with bounding box coordinates and classification. This tool identifies multiple objects in images providing precise location data and object categories with confidence scores. Returns object detection results as JSON. Requires resource-name and image-url or image-data.
- `azmcp-ai-vision-imageanalysis-text-read` - Extract text from images using optical character recognition (OCR) with layout preservation. This tool performs accurate text extraction from images including handwritten and printed text with spatial positioning and reading order. Returns extracted text as JSON. Requires resource-name and image-url or image-data.

**Suggested Prompts:**
- "Analyze this image and describe what you see: <image-url>"
- "Extract all text from this screenshot using OCR"
- "Detect and identify all objects in this photo: <photo-url>"
- "Generate descriptive tags for this product image"

### Communication Tools (New Namespace: `communication`) ⚠️

> **Note**: The `communication` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Communication.Email
- `azmcp-communication-email-send` - Send emails through Azure Communication Services with rich content and attachment support. This tool delivers emails with HTML/text content, file attachments, and delivery tracking capabilities using Azure's email infrastructure. Returns email operation status and message ID. Requires service-name, recipient-email, subject, and message-content.
- `azmcp-communication-email-status-get` - Get delivery status and tracking information for sent emails. This tool monitors email delivery progress including sent, delivered, bounced, and failed status with detailed tracking information and error codes. Returns email status as JSON. Requires service-name and message-id.

**Suggested Prompts:**
- "Send an email to <recipient> with subject <subject> and message <message>"
- "Check the delivery status of email with ID <email-id>"

- [ ] #### Azure.Communication.Chat
- `azmcp-communication-chat-thread-create` - Create new chat threads for real-time messaging with multiple participants. This tool initializes chat conversations with configurable participant lists, thread topics, and message retention policies. Returns thread ID and creation details as JSON. Requires service-name, thread-topic, and participant-list.
- `azmcp-communication-chat-message-send` - Send messages to existing chat threads with rich content and metadata support. This tool delivers text messages, file attachments, and system notifications to chat participants with delivery confirmation. Returns message ID and send status. Requires service-name, thread-id, and message-content.
- `azmcp-communication-chat-participants-list` - List all participants in a chat thread with their roles and status information. This tool retrieves participant details including display names, user IDs, join dates, and current online status. Returns participant information as JSON array. Requires service-name and thread-id.

**Suggested Prompts:**
- "Create a new chat thread with participants <participant-list>"
- "Send message <message> to chat thread <thread-id>"
- "List all participants in chat thread <thread-id>"

- [ ] #### Azure.Communication.Sms
- `azmcp-communication-sms-send` - Send SMS messages to mobile phone numbers through Azure Communication Services. This tool delivers text messages with delivery confirmation and supports both single and bulk SMS operations with customizable sender IDs. Returns message ID and send status. Requires service-name, recipient-phone-number, and message-content.
- `azmcp-communication-sms-delivery-report` - Get comprehensive delivery reports and status information for sent SMS messages. This tool tracks message delivery including sent, delivered, failed, and undelivered status with detailed error codes and delivery timestamps. Returns delivery report as JSON. Requires service-name and message-id.

**Suggested Prompts:**
- "Send SMS message <message> to phone number <phone-number>"
- "Check the delivery status of SMS message <message-id>"

### Messaging Tools (New Namespace: `messaging`) ⚠️

> **Note**: The `messaging` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Messaging.EventGrid
- `azmcp-messaging-eventgrid-events-publish` - Publish custom events to Event Grid topics for event-driven architectures. This tool sends structured event data to Event Grid topics with schema validation and delivery guarantees for downstream subscribers. Returns publish operation status. Requires topic-name, event-data, and optional event-schema.
- `azmcp-messaging-eventgrid-topics-list` - List all Event Grid topics in a subscription with configuration and status information. This tool retrieves topic details including endpoints, access keys, and subscription information for event publishing and management. Returns topic information as JSON array. Requires subscription-id.
- `azmcp-messaging-eventgrid-subscriptions-list` - List event subscriptions for topics with filtering and endpoint configuration. This tool shows all active subscriptions including webhook endpoints, event filters, and delivery retry policies. Returns subscription details as JSON array. Requires topic-name or subscription-id.

**Suggested Prompts:**
- "Publish an event with data <event-data> to Event Grid topic <topic-name>"
- "List all Event Grid topics in my subscription"
- "Show me all event subscriptions for topic <topic-name>"

- [ ] #### Azure.Messaging.EventHubs
- `azmcp-messaging-eventhubs-events-send` - Send high-throughput events to Event Hubs for real-time data streaming and analytics. This tool publishes events to Event Hub partitions with configurable batching, partitioning strategies, and delivery guarantees. Returns send operation status. Requires event-hub-name, event-data, and optional partition-key.
- `azmcp-messaging-eventhubs-events-receive` - Receive and process events from Event Hubs with configurable consumption patterns. This tool retrieves events from Event Hub partitions with checkpoint management and consumer group coordination. Returns event data as JSON array. Requires event-hub-name, consumer-group, and optional partition-id.
- `azmcp-messaging-eventhubs-partitions-list` - List all partitions in an Event Hub with status and metadata information. This tool retrieves partition details including partition IDs, offset information, and message counts for monitoring and management. Returns partition information as JSON array. Requires event-hub-name.

**Suggested Prompts:**
- "Send event with payload <event-payload> to Event Hub <event-hub-name>"
- "Receive the latest events from Event Hub <event-hub-name>"
- "List all partitions for Event Hub <event-hub-name>"

### Container Tools (New Namespace: `containers`) ⚠️

> **Note**: The `containers` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Containers.ContainerRegistry
- `azmcp-containers-registry-repositories-list` - List all repositories in a container registry with metadata and status information. This tool retrieves repository names, creation dates, last updated timestamps, and image counts for registry management and monitoring. Returns repository information as JSON array. Requires registry-name.
- `azmcp-containers-registry-tags-list` - List all tags for a specific repository with detailed image metadata. This tool shows available image tags including creation dates, manifest digests, and image sizes for version management and deployment. Returns tag information as JSON array. Requires registry-name and repository-name.
- `azmcp-containers-registry-manifest-get` - Get detailed manifest information for container images including layer details and configuration. This tool retrieves image manifests with layer information, architecture details, and security metadata for image analysis and deployment planning. Returns manifest data as JSON. Requires registry-name, repository-name, and tag-or-digest.

**Suggested Prompts:**
- "List all repositories in container registry <registry-name>"
- "Show all tags for repository <repository-name> in registry <registry-name>"
- "Get the manifest for image <image-name>:<tag> in registry <registry-name>"

### Security Tools (Existing `keyvault` namespace + New `security` namespace) ⚠️

> **Note**: The `security` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Security.KeyVault.Certificates (extends existing `keyvault` namespace)
- `azmcp-keyvault-certificate-create` - Create new certificates in Key Vault with configurable properties and policies. This tool generates certificates with specified key types, validity periods, and certificate authorities, supporting both self-signed and CA-signed certificates. Returns certificate creation status and thumbprint. Requires vault-name, certificate-name, and certificate-policy.
- `azmcp-keyvault-certificate-import` - Import existing certificates and private keys into Key Vault securely. This tool uploads certificates from various formats (PFX, PEM) with password protection and access policy configuration. Returns import status and certificate details. Requires vault-name, certificate-name, and certificate-data.
- `azmcp-keyvault-certificate-renew` - Renew certificates before expiration with automated or manual processes. This tool handles certificate renewal using existing policies or updated configurations, maintaining continuity for applications and services. Returns renewal status and new certificate details. Requires vault-name and certificate-name.
- `azmcp-keyvault-certificate-export` - Export certificates from Key Vault in various formats for external use. This tool retrieves certificates with or without private keys in PEM, PFX, or DER formats for deployment and backup purposes. Returns certificate data in requested format. Requires vault-name, certificate-name, and export-format.

**Suggested Prompts:**
- "Create a new self-signed certificate named <cert-name> in Key Vault <vault-name>"
- "Import certificate from file <cert-file> into Key Vault <vault-name>"
- "Renew the certificate <cert-name> in Key Vault <vault-name>"

- [ ] #### Azure.Security.KeyVault.Administration (extends existing `keyvault` namespace)
- `azmcp-keyvault-admin-backup-create` - Create comprehensive backups of entire Key Vault contents including keys, secrets, and certificates. This tool performs full vault backups with encryption and secure storage for disaster recovery and compliance requirements. Returns backup operation status and location. Requires vault-name and backup-storage-url.
- `azmcp-keyvault-admin-backup-restore` - Restore Key Vault from complete backups with data integrity verification. This tool restores all vault contents from encrypted backups, handling key restoration, secret recovery, and certificate import with validation. Returns restore operation status. Requires vault-name and backup-location.
- `azmcp-keyvault-admin-settings-get` - Get comprehensive Key Vault administration settings and configuration details. This tool retrieves vault policies, access configurations, network settings, and administrative properties for management and auditing. Returns administration settings as JSON. Requires vault-name.

**Suggested Prompts:**
- "Create a full backup of Key Vault <vault-name>"
- "Restore Key Vault <vault-name> from backup <backup-location>"
- "Show me the administration settings for Key Vault <vault-name>"

- [ ] #### Azure.Security.ConfidentialLedger (new `security` namespace)
- `azmcp-security-confidentialledger-entries-append` - Append tamper-proof entries to the Confidential Ledger with cryptographic verification. This tool adds new entries to the immutable ledger with automatic integrity verification and consensus validation. Returns transaction ID and append confirmation. Requires ledger-name and entry-data.
- `azmcp-security-confidentialledger-entries-get` - Retrieve entries from the Confidential Ledger with integrity verification and audit trails. This tool fetches ledger entries with cryptographic proof of authenticity and tamper detection. Returns entry data with verification status as JSON. Requires ledger-name and optional transaction-id or entry-range.
- `azmcp-security-confidentialledger-receipt-get` - Get cryptographic transaction receipts providing proof of ledger operations. This tool retrieves receipts with digital signatures and merkle proof data for transaction verification and compliance auditing. Returns receipt data with cryptographic proofs as JSON. Requires ledger-name and transaction-id.

**Suggested Prompts:**
- "Add a new entry with data <entry-data> to Confidential Ledger <ledger-name>"
- "Retrieve entries from Confidential Ledger <ledger-name> for transaction <transaction-id>"
- "Get the receipt for transaction <transaction-id> in ledger <ledger-name>"

### Monitor Tools (Existing Namespace: `monitor`)

- [ ] #### Azure.Monitor.Ingestion
- `azmcp-monitor-ingestion-logs-upload` - Upload custom log data to Azure Monitor workspaces using data collection rules. This tool sends structured log data to Azure Monitor with schema validation and transformation support for custom monitoring scenarios. Returns ingestion operation status and record count. Requires workspace-id, data-collection-rule, and log-data.
- `azmcp-monitor-ingestion-data-validate` - Validate log data format and schema compliance before ingestion to prevent errors. This tool checks log data against data collection rule schemas and Azure Monitor requirements to ensure successful ingestion. Returns validation results with error details as JSON. Requires data-collection-rule and log-data.
- `azmcp-monitor-ingestion-status-check` - Check the status and progress of log ingestion operations with detailed monitoring. This tool tracks ingestion jobs providing success rates, error counts, and processing times for troubleshooting and monitoring. Returns ingestion status as JSON. Requires workspace-id and optional operation-id.

**Suggested Prompts:**
- "Upload custom log data <log-data> to Azure Monitor workspace <workspace-name>"
- "Validate this log data before sending to Azure Monitor: <log-data>"
- "Check the ingestion status for data collection rule <rule-name>"

### Data Tools (New Namespace: `data`) ⚠️

> **Note**: The `data` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Data.SchemaRegistry
- `azmcp-data-schemaregistry-schemas-list` - List all schemas in a Schema Registry with metadata and version information. This tool retrieves schema names, formats, compatibility settings, and version history for schema management and governance. Returns schema list as JSON array. Requires registry-name.
- `azmcp-data-schemaregistry-schema-register` - Register new schemas in the Schema Registry with validation and compatibility checking. This tool uploads schema definitions (Avro, JSON Schema, etc.) with version management and backward compatibility validation. Returns schema registration status and ID. Requires registry-name, schema-name, and schema-definition.
- `azmcp-data-schemaregistry-schema-get` - Get specific schema definitions and metadata from the Schema Registry. This tool retrieves complete schema information including definition, version details, and compatibility settings for development and validation. Returns schema data as JSON. Requires registry-name and schema-name or schema-id.
- `azmcp-data-schemaregistry-schema-validate` - Validate data against registered schemas to ensure compliance and data quality. This tool checks data format and structure against schema definitions providing validation errors and compliance status. Returns validation results as JSON. Requires registry-name, schema-name, and data-to-validate.

**Suggested Prompts:**
- "List all schemas in Schema Registry <registry-name>"
- "Register a new schema named <schema-name> with definition <schema-definition>"
- "Get the schema definition for schema <schema-name>"
- "Validate this JSON data against schema <schema-name>: <json-data>"

### Health Tools (New Namespace: `health`) ⚠️

> **Note**: The `health` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Health.Deidentification
- `azmcp-health-deidentification-text-process` - De-identify text containing protected health information (PHI) using HIPAA-compliant methods. This tool removes or masks sensitive health data including names, dates, addresses, and medical identifiers while preserving clinical context. Returns de-identified text with redaction summary. Requires service-name and text-content.
- `azmcp-health-deidentification-job-submit` - Submit batch de-identification jobs for processing multiple documents containing health information. This tool handles large-scale PHI removal from document collections with configurable redaction policies and output formats. Returns job ID and submission status. Requires service-name, source-location, and job-configuration.
- `azmcp-health-deidentification-job-status` - Check status and progress of batch de-identification jobs with detailed processing metrics. This tool monitors job execution providing completion percentage, processing times, and error reports for batch operations. Returns job status as JSON. Requires service-name and job-id.

**Suggested Prompts:**
- "De-identify this medical text: <medical-text>"
- "Submit a batch de-identification job for documents in <source-location>"
- "Check the status of de-identification job <job-id>"

### IoT Tools (New Namespace: `iot`) ⚠️

> **Note**: The `iot` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.DigitalTwins.Core
- `azmcp-iot-digitaltwins-models-list` - List all digital twin models in an Azure Digital Twins instance with their definitions and metadata. This tool retrieves model schemas, relationships, and properties for digital twin development and management. Returns model information as JSON array. Requires instance-name.
- `azmcp-iot-digitaltwins-instances-create` - Create new digital twin instances based on registered models with custom properties and relationships. This tool instantiates digital twins with initial property values and metadata for IoT device representation. Returns twin creation status and ID. Requires instance-name, model-id, and twin-properties.
- `azmcp-iot-digitaltwins-instances-query` - Query digital twin instances using SQL-like syntax with filtering and relationship traversal. This tool searches twins based on properties, relationships, and metadata with support for complex queries and aggregations. Returns query results as JSON array. Requires instance-name and query-expression.
- `azmcp-iot-digitaltwins-relationships-create` - Create relationships between digital twin instances to model real-world connections and hierarchies. This tool establishes typed relationships with properties and metadata for representing complex IoT ecosystems. Returns relationship creation status and ID. Requires instance-name, source-twin-id, target-twin-id, and relationship-type.

**Suggested Prompts:**
- "List all digital twin models in instance <instance-name>"
- "Create a new digital twin of type <model-type> with ID <twin-id>"
- "Query for all digital twins with property <property-name> = <value>"
- "Create a relationship between twin <twin1-id> and twin <twin2-id>"

- [ ] #### Azure.IoT.DeviceUpdate
- `azmcp-iot-deviceupdate-updates-list` - List all available device updates and firmware versions with compatibility information. This tool retrieves update packages, version details, and device compatibility matrices for IoT device management. Returns update information as JSON array. Requires instance-name and optional device-type.
- `azmcp-iot-deviceupdate-deployment-create` - Create device update deployments with scheduling and rollback capabilities. This tool initiates update deployments to device groups with configurable rollout strategies and failure handling policies. Returns deployment ID and configuration status. Requires instance-name, update-id, device-group, and deployment-configuration.
- `azmcp-iot-deviceupdate-deployment-status` - Check deployment status and progress with detailed device-level reporting. This tool monitors update deployments providing success rates, failure analysis, and device-specific status information. Returns deployment status as JSON. Requires instance-name and deployment-id.

**Suggested Prompts:**
- "List all available updates for device type <device-type>"
- "Deploy update <update-id> to device group <device-group>"
- "Check the status of deployment <deployment-id>"

### Mixed Reality Tools (New Namespace: `mixedreality`) ⚠️

> **Note**: The `mixedreality` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.MixedReality.RemoteRendering
- `azmcp-mixedreality-remoterendering-session-create` - Create remote rendering sessions for high-fidelity 3D content streaming. This tool initializes cloud-based rendering sessions with configurable compute resources and session duration for mixed reality applications. Returns session ID and connection details. Requires service-name, session-size, and optional session-configuration.
- `azmcp-mixedreality-remoterendering-session-status` - Check status and health of active remote rendering sessions with performance metrics. This tool monitors session state including GPU utilization, network latency, and rendering quality for optimization and troubleshooting. Returns session status as JSON. Requires service-name and session-id.
- `azmcp-mixedreality-remoterendering-models-convert` - Convert 3D models to optimized formats for remote rendering with quality settings. This tool processes 3D assets (FBX, glTF, etc.) for cloud rendering optimization including LOD generation and texture compression. Returns conversion job status and output location. Requires service-name, model-url, and conversion-settings.

**Suggested Prompts:**
- "Create a new remote rendering session with size <session-size>"
- "Check the status of remote rendering session <session-id>"
- "Convert 3D model <model-url> for remote rendering"

### DevOps Tools (New Namespace: `devops`) ⚠️

> **Note**: The `devops` namespace does not exist yet in the current Azure MCP server implementation.

- [ ] #### Azure.Developer.LoadTesting
- `azmcp-devops-loadtesting-tests-create` - Create comprehensive load tests with configurable user patterns and performance criteria. This tool designs load testing scenarios with virtual user simulation, ramp-up profiles, and performance thresholds for application testing. Returns test ID and configuration status. Requires service-name, test-configuration, and target-endpoints.
- `azmcp-devops-loadtesting-tests-run` - Execute load tests with real-time monitoring and automatic scaling capabilities. This tool runs performance tests with live metrics collection, automatic result analysis, and configurable test duration and load patterns. Returns test run ID and execution status. Requires service-name and test-id.
- `azmcp-devops-loadtesting-results-get` - Get detailed load test results with performance metrics and analysis reports. This tool retrieves comprehensive test outcomes including response times, throughput, error rates, and performance bottleneck identification. Returns test results as JSON. Requires service-name and test-run-id.

**Suggested Prompts:**
- "Create a load test for URL <target-url> with <concurrent-users> concurrent users"
- "Run the load test <test-name> and show me the results"
- "Get the detailed results for load test run <run-id>"

- [ ] #### Azure.Developer.DevCenter
- `azmcp-devops-devcenter-environments-list` - List all available development environments and templates with configuration details. This tool retrieves environment definitions including resource specifications, available templates, and deployment options for development teams. Returns environment list as JSON array. Requires devcenter-name.
- `azmcp-devops-devcenter-environment-create` - Create new development environments from templates with custom configurations. This tool provisions development resources including VMs, databases, and networking based on predefined templates with user customizations. Returns environment creation status and access details. Requires devcenter-name, template-id, and environment-configuration.
- `azmcp-devops-devcenter-projects-list` - List all Dev Center projects with team assignments and resource allocations. This tool retrieves project information including team memberships, resource quotas, and access policies for project management and administration. Returns project list as JSON array. Requires devcenter-name.

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