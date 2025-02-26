# SvnDump2Git  Convert Your SVN Dump Files to Git Repositories

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub release](https://img.shields.io/github/v/release/Svn2GitTools/SvnDump2Git)](https://github.com/Svn2GitTools/SvnDump2Git/releases/latest)

**SvnDump2Git** is a command-line tool written in C# that converts Subversion (SVN) dump files into Git repositories. It leverages the [SvnDumpParser](https://github.com/your-svn-dump-parser-repo-link) library to parse SVN dump files and [GitImporter](https://github.com/your-git-importer-repo-link) (or your Git interaction library) to create a Git repository.

This tool provides two conversion modes: full in-memory conversion for smaller repositories and a step-by-step conversion for handling very large dumps more efficiently.

## Features

*   **SVN Dump to Git Conversion:** Converts SVN dump files to fully functional Git repositories.
*   **Full In-Memory Conversion:** Suitable for smaller SVN repositories, loading all revisions into memory before Git conversion.
*   **Step-by-Step Conversion:** Processes the SVN dump revision by revision, making it memory-efficient for large repositories.
*   **Command-Line Interface:** Easy to use command-line interface for conversion.
*   **Author Mapping Support:**  Supports mapping SVN authors to Git authors using an authors map file.
*   **Verbose Mode:** Provides detailed output during the parsing process using the `-v` flag.

## Getting Started

### Prerequisites

*   [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0 or higher - specify the required version)

### Installation

1.  **Clone the repository:**

    ```bash
    git clone <repository-url>
    cd SvnDump2Git
    ```

2.  **Build the project:**

    ```bash
    dotnet build
    ```

    This will create the executable in the `SvnDump2Git/bin/Debug/netX.X` or `SvnDump2Git/bin/Release/netX.X` directory (depending on your build configuration and .NET version).

### Usage

Navigate to the output directory containing the executable (e.g., `SvnDump2Git/bin/Debug/netX.X`).

```bash
cd SvnDump2Git/bin/Debug/net9.0
```

Run the `SvnDump2Git` tool with the following command-line arguments:

```bash
SvnDump2Git.exe <dump_file> [--git <git_repo_path>] [--full-inmemory] [-v]
```
**Command-Line Switches:**

*   `<dump_file>`: **(Required)**  The path to your SVN dump file. This is the input file for the conversion process.
*   `--git <git_repo_path>`: **(Optional)**  Specifies the directory where the new Git repository will be created. If not provided, the Git repository will be initialized in the current working directory.
*   `--full-inmemory`: **(Optional)**  Enables full in-memory conversion mode. This mode loads the entire SVN dump into memory before converting it to Git.  It's recommended for smaller to medium-sized repositories. For very large repositories, step-by-step conversion (the default) is more memory-efficient.
*   `-v`: **(Optional)** Enables verbose output during the parsing and conversion process. This will provide more detailed information in the console about the tool's progress and any potential issues.

**Examples:**

1.  **Basic Conversion (step-by-step, Git repo in the current directory):**

    ```bash
    SvnDump2Git.exe my_svn_dump.dump
    ```

2.  **Conversion to a specific Git repository path (step-by-step):**

    ```bash
    SvnDump2Git.exe my_svn_dump.dump --git ./my-git-repo
    ```

3.  **Full In-Memory Conversion:**

    ```bash
    SvnDump2Git.exe my_svn_dump.dump --full-inmemory --git ./my-git-repo
    ```

4.  **Verbose Output with Git Conversion:**

    ```bash
    SvnDump2Git.exe my_svn_dump.dump --git ./my-git-repo -v
    ```

**Important Notes:**

*   Replace `<repository-url>`, `<your-svn-dump-parser-repo-link>`, and `<your-git-importer-repo-link>` with the actual URLs.
*   Specify the correct .NET SDK version in the Prerequisites section.
*   If you have author mapping functionality, describe how to use the `--authors-map <authors_map_file>` option (if implemented) in the Usage section and Features.
*   Consider adding error handling and more detailed usage instructions if your tool has more options or complexities.

