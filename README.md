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

    ### ✅ Progress Update: Sprint 1 Complete
- [x] Initial Solution & Project Architecture (.NET 8)
- [x] TDD Suite implemented (xUnit & Moq)
- [x] HiringCafeParser (Scribe) validated with live payloads
- [x] Multi-Lens Triage Logic (Support vs. Engineering)
- [x] Secure Secret Injection via User Secrets & Environment Variables
- [x] CI/CD Pipeline (GitHub Actions) configured for automated testing

    ### 🔐 Security & Configuration
This project uses the **.NET Configuration Provider** to handle sensitive API keys.

1. **Local:** Secrets are managed via `dotnet user-secrets`.
2. **CI/CD:** Secrets are stored in GitHub Actions as `GEMINI_API_KEY` and injected into the test environment as `Gemini__ApiKey`.
3. **Production:** The Worker Service reads from Environment Variables on the host machine.