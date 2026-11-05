<p align="center">
  <img src="docs/images/JingPu-logo.png" alt="JingGenogram Logo" width="320"/>
</p>

<h1 align="center">JingGenogram</h1>

<p align="center">
  <img src="https://img.shields.io/badge/version-1.5.0-blue" alt="version"/>
  <img src="https://img.shields.io/badge/license-MIT-green" alt="license"/>
  <img src="https://img.shields.io/badge/platform-Windows%2010%2F11-0078D4" alt="platform"/>
  <img src="https://img.shields.io/badge/.NET%20Framework-4.5%2B-68217A" alt="framework"/>
</p>

<p align="center">An offline, open-source Chinese family-archive and pedigree-diagram tool — keeping family memory traceable and every name remembered.</p>

---

## Introduction

**JingGenogram** (景·谱) is an offline Chinese genealogy & family-tree manager that supports multiple independent family trees. It brings member profiles, kinship, pedigree diagrams, family chronicles and archives together in one place — ideal for family record-keeping, clan-tree organization and heritage preservation. All content is edited by you; the software never judges historical authenticity on its own.

JingGenogram is an offline, open-source Chinese genealogy & family-tree manager. It keeps member profiles, kinship, pedigree diagrams, family chronicles and archives in one place — fully offline, no account or network required.

> 🔗 Repository: **https://github.com/jingpanlong/JingGenogram**
> The in-app "About" page also embeds this address with one-click open/copy.

---

## Features

- **Multiple family trees**: each tree has its own directory with independent attachments and backups; supports create, duplicate, switch, rename, and add-from-ZIP.
- **Recycle & permanent delete**: the recycle area can be restored; v1.5 adds "delete selected permanently" and "empty recycle area", requiring you to type the full name to confirm, preventing accidental deletion.
- **Member profiles**: avatar, biography, multiple spouses and parent/child relationships, full-text search and filtering.
- **Kinship lookup**: query the kinship path between any two people.
- **Pedigree diagrams**: family-group pedigree charts with three themes (Ink / Jade / Indigo), detailed / compact cards, photos, ancestor / descendant / close-relative views; supports SVG / EMF vector export, interactive web pages, high-res / transparent PNG, and paginated printing / PDF.
- **Editing workbench**: quick-add relatives, duplicate merge, batch completion, and an editing to-do list.
- **Family chronicle & collection**: birthday / anniversary reminders, generational-name and origin statistics, and attachment management.
- **Data interchange**: editable Excel export and re-import by ID, Excel / CSV template download and import, JSON / GEDCOM import and export, CSV export, HTML family album, and ZIP full backup & restore.
- **Fully offline**: no family content is uploaded; no network or registration required.

---

## Screenshots

| Tree overview | Member profile | Pedigree diagram |
| --- | --- | --- |
| ![Tree overview](docs/images/家谱概览.png) | ![Member profile](docs/images/成员档案.png) | ![Pedigree diagram](docs/images/世系图谱.png) |

| Editing workbench | Family chronicle | Data management |
| --- | --- | --- |
| ![Editing workbench](docs/images/编修工作台.png) | ![Family chronicle](docs/images/家族大事记.png) | ![Data management](docs/images/数据管理.png) |

> The "About Software" page embeds the GitHub repository address with one-click open/copy:
>
> ![About Software](docs/images/关于软件.png)

---

## Download

Download the latest version from **[Releases](https://github.com/jingpanlong/JingGenogram/releases)**:

| File | Description |
| --- | --- |
| `JingGenogram-1.5.0-setup.exe` | Installer (recommended): includes Start Menu entry and uninstaller |
| `JingGenogram-1.5.0.exe` | Single-file build: no install, double-click to run |

- **Requirements**: Windows 10 / 11 with the system's built-in .NET Framework 4.5 or later (no extra runtime needed).
- **Fully offline**: no network or registration required.
- After downloading, we recommend verifying the checksums in `SHA256SUMS.txt`.

---

## Build from Source

The source is compiled with the system's built-in Windows .NET Framework C# compiler — no third-party runtime dependencies.

```powershell
# In Windows PowerShell, enter the source directory and run:
cd src
powershell -ExecutionPolicy Bypass -File .\build.ps1
```

Build output is in `src/dist/`:

- `JingFamily.exe` — single-file main program
- `JingFamily-Setup.exe` — installer embedding the main program

> The build needs `JingPu-icon-256.png` (provided with the repo); `build.ps1` references it automatically.

---

## Data & Privacy

- This software **does not upload** family content. Please obtain consent before entering contact info, addresses, or photos of living relatives.
- Full backups and JSON contain personal profiles and are unencrypted — keep them in a trusted location; CSV also includes contact info.
- Birthdays are calculated by the Gregorian calendar; if the date is unknown, only the year or year-month may be filled.
- GEDCOM is read/written in UTF-8 and covers basic person and family relationships.
- System printing can use an installed PDF printer; attachments open with the corresponding program on your system.
- Uninstalling keeps the family root directory by default (located at `%LOCALAPPDATA%\JingFamily`).

---

## License

This project is open-sourced under the **MIT License** — see [LICENSE](LICENSE).

---

## Contact

- WeChat Official Account: 景同志
- Xiaohongshu: 景同学

Feedback on usage and family-tree editing is welcome. You can also open an Issue on GitHub or contribute improvements.
