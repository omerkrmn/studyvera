# PFD Insights Log

Running log of non-obvious findings from every PFD analysis.
Reviewed periodically. Candidates get promoted to `accumulated-learnings.md`;
the rest stay as searchable history.

**Format:**
```markdown
### YYYY-MM-DD: [Brief description of what was analyzed]
**Type:** url | text | image | html | css | copy | directory
**Domain:** [e.g., SaaS landing, ecommerce PDP, email, portfolio, dashboard, presentation]
**Key finding:** [The non-obvious thing PFD surfaced, one sentence]
**Layer(s):** [Which PFD layer(s) this relates to: Foundation/L1/L2/L3/L4]
**Promote?:** yes | maybe | no
**Notes:** [Optional: context, cross-references to prior findings, patterns noticed]
```

---

<!-- New entries go here, newest first -->

### 2026-09-18: StudyVera student dashboard profile page redesign
**Type:** directory
**Domain:** EdTech dashboard
**Key finding:** Deficiency topics framed with danger/fire icons trigger defensive avoidance rather than study motivation; reframing as "growth opportunities" with upward-arrow icons shifts the perception from threat to progress — the same data, presented through L3-aware framing, changes user behavioral response from avoidance to approach.
**Layer(s):** L0/L3/L4
**Promote?:** maybe
**Notes:** Secondary finding: quote cards with random motivational text suffer banner blindness after first visit, consuming L0 bandwidth without providing value. Score/rank data duplicated across 3 locations (hero card, stat cards, sidebar) directly violates WM chunk limit. Cross-reference with L0 progressive disclosure principle — collapsing identical data into single hero surface freed 3+ WM chunks.
