The Automated Workforce
1. The Sentinel (n8n Workflow)

    Role: Traffic Controller.

    Responsibility: Authentication with Google Services, managing the timing of the workflow, and handling the "Human-in-the-loop" notification via Telegram.

2. The Scribe (.NET Parser Service)

    Role: Data Extraction Specialist.

    Logic: Uses HtmlAgilityPack to transform messy email HTML into clean C# Records.

    TDD Constraint: Must pass unit tests for "Empty Email," "Malformed HTML," and "Multiple Job Entries."

3. The Architect (C# + Gemini API)

    Role: Persona Strategist.

    Logic: * IF (Keywords: Support, Admin, Infrastructure, Identity, Helpdesk) → Use Scale AI/Resilient Engineering Master.

        ELSE → Use SnowCloud/Full Stack Master.

    TDD Constraint: Mock the Gemini API response to ensure the service handles "API Down" or "Quota Exceeded" scenarios gracefully.

4. The Operator (GitHub Actions)

    Role: Quality Assurance & Deployment.

    Workflow: * On git push: Run all xUnit tests.

        On test pass: Build the project and (optional) deploy the container to your local Docker host.

        #### 🤖 Status: Operational
- **The Scribe:** Optimized with HtmlAgilityPack. Now capable of parsing 20+ jobs from complex HTML MIME sources.
- **The Architect:** Integrated with Gemini 1.5 Flash. Prompting logic updated to reference the 'Scale AI' Support Master and 'SnowCloud' Engineering Master based on Triage results.
- **The Operator:** GitHub Actions successfully running 'dotnet test' on every push.