# CLIN(Command Line Interface Notes)
Fast and no clutter notes app that works right in your terminal!

## Features:
* **Fast:** Lightweight CLI designed for quick terminal use.
* **Simple:** Clean, intuitive command-line interface.
* **No distractions:** Pure terminal workflow with offline JSON persistence.

## Installation:

### Requirements:
**OS:** Linux(x86_64) 
**No .NET runtime is required as CLIN is distributed as a self-contained executable**

### Method 1: Install via GitHub Release(Recommended):
- *This is the simplest way for anyone to install and it works for most people.*

1. **Binary Installation**:
- *In my release page, download the latest version of CLIN.*

2. **Extract and install system-wide**:
- *Run the following commands in order:*
```bash
unzip clin-vx.x-linux-x64.zip
sudo cp CLIN /usr/local/bin/clin
sudo chmod +x /usr/local/bin/clin
```

*In the first command, replace the x.x in version with the version you have installed.*
**For Examaple:**
```bash
unzip clin-v1.0-linux-x64.zip
```
*that should do it and you'll be ready to use CLIN*

### Method 2: Building from Source(Not recommended for most people):
- **Before continuing any of the steps given below, ensure that you have .NET 8.0 SDK installed otherwise the process will not work.**

1. **Clone the repository and go to it on terminal:**
- *Run the command:*
```bash
git clone https://github.com/PhaseShiftX0/CLIN.git
cd CLIN
```
2. **Publish the single-file binary:**
- *Run the command:*
```bash
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
```
3. **Install to system path:**
- *run the following commands, in order:*
```bash
sudo cp bin/Release/net8.0/linux-x64/publish/CLIN /usr/local/bin/clin
sudo chmod +x /usr/local/bin/clin
```
## Verify the installation:
**To verify whether CLIN is working or not, run this command:**
```bash
clin help
```
*You should get the following output if everything went well:*
```text
Thanks for choosing CLIN!
I hope You have a good experience and give me support on GitHub!

Usage: CLIN <command> "<title>" "<note>"
---
Available commands:

CLIN new "<title>" "<note>": create a new note.
CLIN open "<title>": view note content.
CLIN view: list all note titles.
CLIN pin "<title>": pin a note.
CLIN unpin "<title>": unpin a note.
CLIN delete "<title>": delete a note.
CLIN help: display this guide.
Note that every note should have a different title, regardless of capitalization.
```

## Troubleshooting:
### Command not found error:
```text
clin: command not found.
```
*Ensure that you have /usr/local/bin is in your shell's PATH. You check it with the command:*

```bash
echo $PATH
```
*Expected output should be as follows:*
```text
/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
```

- *Here, you should find **"/usr/local/bin"** somewhere in the output.*

### How to fix it:
- *if /usr/local/bin is missing in the output then add it permanently using the commands below:*
```bash
echo 'export PATH="/usr/local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
```
- **Note**: *if you use zsh instead of bash, replace "~/.bashrc" with "~/.zshrc".*

### Permission denied error:
- *This error happens when the binary was copied to /usr/local/bin but it lacks the execution privileges for your account. The error should give the output:*

```text
bash: /usr/local/bin/clin: Permission denied
```
- **Note:** if you use zsh then instead of bash, it will show zsh.

### How to fix it:
- *Run the following command in order to gain the permission to execute it:*
```bash
sudo chmod +x /usr/local/bin/clin
```
- *this grants you the global permission to execute the binary.*

### CLIN crash upon start when user runs the view command:
- *If CLIN behaves unexpectedly because of .notes.json, it means that it contains invalid data.*
- *The error output will a giant dump of errors, but you can tell when it happens*

### How to fix it:
- *Run this command to reset your notes.json file into a valid empty json array:*

```bash
echo "[]" > ~/.clin_notes.json
```

[!CAUTION] All your previous notes will be reduced to atoms.

### Important Notes:
- if  you use zsh instead of bash then every error and command mentioned above will replace "bash" with "zsh" just as you saw in the Permission Denied Error.
- If none of the troubleshooting methods work or if a error is not covered then open a issue on GitHub or seek further help at my E-mail: **phaseshiftx0@gmail.com**, I'll try my best to reply and help you out.

## Usage & Examples:
1. **To create a new note**:

- *To create a new note, run the following command*

```bash
clin new "Title" "Note"
```

- **clin:** *calls the binary to execute this command.*
- **new:** *it is the command that tells CLIN that a new note is being created.*
- **Title:** *it is the placeholder for your note, it tells this note shall contain this title. Insert your own title there.*
- **Note:** *it is the actual content of your note. Insert your own note there.*
- **Additional:** *Remember to use quotations("") to wrap your title and note and to us different titles for each note otherwise an error will show up.*
**For Example:**

```bash
clin new "To-do today" "Go to PhaseShiftX0's github page and give a star to CLIN."
```
**output**:
```text
Added Note: To-Do today
```

2. **To open a file and view its content:**
- *To open a note and view its content, run the following command:*

```bash
clin open "title"
```
- **clin:** *calls the binary to execute this command*
- **open:** *it is the command that tells CLIN to open a note*
- **title:** *here you write the title of the note you wanna open. To see all the titles of the notes you've created, you will need to use view command which is up next after this.*

**For Example:**

```bash
clin open "To-do today"
```

**Output:**

```text
Title: To-do today

Content: Go to PhaseShiftX0's github page and give a star to CLIN.
```

3. **To view all notes' titles:**
*to see the title of every note you have created, run the following command:*

```bash
clin view
```

- **clin:** *calls the binary to execute this command*
- **view:** *grabs all of your note(s)'s titles and displays them.*

**For Example:**

```bash
clin view
```

**Output:**

```text
1. To-do today
```

*if a note is pinned then it will be displayed first and have the output as follows:*

```text
1. [PINNED] To-do today
```

*if no note exists yet then the output will be as follows:*

```text
No notes found.
```

4. **To pin a note:**
- *In order to pin a note, run the following command:*

```bash
clin pin "title"
```

- **clin:** *calls the binary to execute this command.*
- **pin:** *command used to pin a task.*
- **title:** *the place where the title of your note that you want to pin goes.*

**For Example:**

```bash
clin pin "To-do today"
```

**Output:**

```text
Pinned note: To-do today
```

5. **To unpin a note**
- *To unpin a note, run this command:*

```bash
clin unpin "title"
```

- **clin:** *calls the binary to execute this command.*
- **unpin:** *The function used to unpin notes.*
- **title:** *title of the note you wanna unpn.*

**For Example:**

```bash
clin unpin "To-do today"
```

**output:**

```text
'To-do today' has been unpinned.
```

6. **To delete a note:**
*To delete a note, run the following command:*

```bash
clin delete "title"
```

- **clin:** *calls the binary to execute this command.*
- **delete:** *Function used to delete notes.*
- **title:** *The title of the note you wanna delete.*

**For Example:**

```bash
clin delete "To-do today"
```

**Output:**

```text
'To-do today' has been deleted.
```

7. **where to look for help if you forget commands:**
*Since we all can sometimes forget commands, run this small command to get a short guide on how to use CLIN:*

```bash
clin help
```

- **clin:** *calls the binary to execute this command.*
- **help:** *function used to help people in the command line interface itself.*

**For Example:**

```bash
clin help
```

**Output:**

```text
Thanks for choosing CLIN!
I hope You have a good experience and give me support on GitHub!

Usage: CLIN <command> "<title>" "<note>"
---
Available commands:

CLIN new "<title>" "<note>": create a new note.
CLIN open "<title>": view note content.
CLIN view: list all note titles.
CLIN pin "<title>": pin a note.
CLIN unpin "<title>": unpin a note.
CLIN delete "<title>": delete a note.
CLIN help: display this guide.
Note that every note should have a different title, regardless of capitalization.
```

*Alright, all of this wraps all the commands that are currently in version 1.0. If I release a new command then I'll update this.*

## Daily use example:
*Here's a short demonstration of how CLIN can be used daily:*

```bash
clin new "Was CLIN made with tutorial"  "No, it was not as I don't follow slow tutorials."
clin new "birthday of my family" "Mom on 2nd February, Dad on 15th August."
clin new "What is photosynthesis" "The process by which plants make food for themselves"
clin view
clin pin "Was CLIN made with tutorial"
clin pin "birthday of my family"
clin delete "What is photosynthesis"
clin unpin "birthday of my family"
clin view
clin open "Was CLIN made with tutorial"
```

**Output:**

```text
Added Note: Was CLIN made with tutorial
Added Note: birthday of my family
Added Note: What is photosynthesis
Here's all your notes:
1. Was CLIN made with tutorial
2. birthday of my family
3. What is photosynthesis
Pinned Note: Was CLIN made with tutorial
Pinned Note: birthday of my family
'What is photosynthesis' has been deleted.
'birthday of my family' has been unpinned.
Here's all your notes:
1. [PINNED] Was CLIN made with tutorial
2. birthday of my family
Title: Was CLIN made with tutorial

Content: No, it was not as I don't follow slow tutorials.

```

*This warps this up, see you next time. Make sure to support me on GitHub. Farewell!*