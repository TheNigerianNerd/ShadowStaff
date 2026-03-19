Project: ShadowStaff

ShadowStaff is an automated, event-driven career assistant designed to ingest job alerts, classify roles, and generate tailored application documents using a multi-lens AI approach.
🛠 The Tech Stack (Zero-Cost & Learning-Focused)

    Orchestration: n8n (Self-Hosted via Docker/Desktop)

    Core Engine: .NET 8 Worker Service (C#)

    API: Minimal API for Webhook ingestion

    Intelligence: Gemini 1.5 Flash (Free Tier API)

    Testing: xUnit / Moq (TDD Approach)

    CI/CD: GitHub Actions (Automated Test & Build)

    Notifications: Telegram Bot API

🔄 The End-to-End Process

    Trigger: n8n monitors Gmail for Hiring Cafe alerts at 2:00 PM.

    Ingestion: n8n POSTs the raw email body to the local C# Service (/ingest).

    Extraction: C# service parses HTML into structured JobOpportunity objects.

    Triage: The system selects the Support Master CV or Engineering Master CV based on job requirements.

    Generation: Gemini API generates Markdown-formatted CVs and Cover Letters.

    Review: n8n sends a Telegram alert with links to the drafts in Google Drive.

    Ship: Upon manual approval, a final PDF is generated for submission.