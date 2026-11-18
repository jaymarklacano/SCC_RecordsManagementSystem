# SCC Records Management System

A Windows Forms desktop application for the Supreme Student Council (SSC) of Southern Leyte State University.
It manages **student records**, **multi-session events**, **student officer accounts**, and **attendance using face recognition** through the computer's webcam.

Everything is written in **C# (Windows Forms, .NET Framework 4.7.2)** with a **MySQL/MariaDB** database.
Face detection and recognition use the **OpenCvSharp4** NuGet package, so no other programming language is needed.

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Main Features](#2-main-features)
3. [System Requirements](#3-system-requirements)
4. [Required Software and Versions](#4-required-software-and-versions)
5. [Visual Studio Requirements](#5-visual-studio-requirements)
6. [Database Setup](#6-database-setup)
7. [How to Import the SQL File](#7-how-to-import-the-sql-file)
8. [How to Configure the Database Connection](#8-how-to-configure-the-database-connection)
9. [NuGet Packages and Exact Versions](#9-nuget-packages-and-exact-versions)
10. [Webcam Requirements](#10-webcam-requirements)
11. [How to Build the Project](#11-how-to-build-the-project)
12. [How to Run the Project](#12-how-to-run-the-project)
13. [How to Register a Student](#13-how-to-register-a-student)
14. [How to Capture the Required 5 Face Images](#14-how-to-capture-the-required-5-face-images)
15. [How Face Recognition Works](#15-how-face-recognition-works)
16. [How to Record Attendance](#16-how-to-record-attendance)
17. [How to View Attendance Records](#17-how-to-view-attendance-records)
18. [Common Errors and Troubleshooting](#18-common-errors-and-troubleshooting)
19. [Project Structure](#19-project-structure)
20. [Known Limitations](#20-known-limitations)

---

## 1. Project Overview

The system lets the Student Council:

- keep a list of students (with their course, year level, and contact details),
- register 5 face pictures of every student,
- schedule events with morning and afternoon attendance sessions on each event day,
- record log in and optional log out attendance automatically when a registered student stands in front of the webcam,
- review attendance by event/session and calculate absences only after a session ends,
- track 10 obligation points per absent session, with recorded deductions and zero-balance adjustments,
- create officer login accounts that use the same sign-in screen but cannot manage officer accounts.

Default login after importing the database: **username `admin`, password `admin123`**.

## 2. Main Features

| Area | What it does |
|------|--------------|
| **Login / Forgot Password** | Admin and officer accounts use the same login screen. Passwords are BCrypt hashes; password resets must be handled by the admin. |
| **Dashboard** | Active student total, present and absent counts for the active event session, current session's attendance, a chart, and current time. |
| **Student Records** | Add, view, edit, and delete students. Search by name or ID and filter by course, year level, or face status. Shows the student's photo. |
| **Student Registration** | Required-field checks, duplicate prevention (ID number, email, same full name), and **webcam registration of exactly 5 face pictures**. A student cannot be saved without all 5 faces. Editing lets you re-register the faces. |
| **Event Management** | Create, edit, search, and delete events. Set dates, venue, participating department or all departments, and optional log out. Each event day can include morning and afternoon sessions with configurable times. |
| **Face Recognition** | Scan only during an active event session; choose Log In or Log Out. The database permits one login and one optional logout per student/session. |
| **Attendance Records** | Filter by event, session, date, department, year, status, or student. Shows event/session, time in, time out, and computed absences. |
| **Departments** | Add, edit, delete, and search the courses/departments. A department that still has students cannot be deleted. |
| **Officer Records** | Admin-only CRUD for officer information and their username/password. Officers sign in through the standard login screen. |
| **Student Obligations** | Every completed absent session adds 10 points. Search and filter by event, department and year; deduct points or clear a balance to zero with a required reason and history. |
| **Reports & Analysis** | Attendance percentage and present/absent totals by event session and department, most-absent students, point totals, date filters, and CSV export. |

Attendance is keyed to the **event session**, not the calendar day. A 2-day event can have four sessions (morning and afternoon on each day). A student can log in once per session and may log out once if needed. Missing attendance is counted as absent only after that session ends.

## 3. System Requirements

- Windows 10 or Windows 11 (64-bit)
- A webcam (built-in or USB)
- At least 4 GB RAM
- About 3 GB of free disk space for Visual Studio and XAMPP

## 4. Required Software and Versions

| Software | Version | Notes |
|----------|---------|-------|
| Visual Studio | 2022 (Community is free) | With the **.NET desktop development** workload |
| .NET Framework | 4.7.2 (Targeting Pack) | The project targets `net472` |
| XAMPP (MariaDB) | 8.x, with MariaDB 10.4 or newer | Provides MySQL and phpMyAdmin |
| Microsoft Visual C++ Redistributable | 2015-2022, **x64** | Needed by the face library (OpenCV). Download: <https://aka.ms/vs/17/release/vc_redist.x64.exe> |

> **Why XAMPP/MariaDB and not MySQL 8?** The documented `MySql.Data 6.9.12` driver was written for older servers and cannot log in to a
> MySQL 8 server that uses the default `caching_sha2_password` login. XAMPP's MariaDB (or MySQL 5.7) works without extra settings.

## 5. Visual Studio Requirements

1. Download **Visual Studio 2022 Community** from <https://visualstudio.microsoft.com/>.
2. In the installer, tick the workload **.NET desktop development**.
3. Open the **Individual components** tab and make sure **.NET Framework 4.7.2 targeting pack** is ticked
   (if it is missing, Visual Studio will tell you when you build).
4. Click **Install** and wait until it finishes.

## 6. Database Setup

1. Install **XAMPP** (<https://www.apachefriends.org/>).
2. Open the **XAMPP Control Panel**.
3. Click **Start** next to **Apache** and **MySQL**. Both names should turn green.
4. Leave XAMPP running while you use the system.

The default XAMPP MySQL user is `root` with **no password** on port **3306**. That is what the project expects.

## 7. How to Import the SQL File

The file is `scc_records_management.sql` (in the same folder as this README).

**Option A - phpMyAdmin (easiest)**

1. Start Apache and MySQL in XAMPP.
2. Open <http://localhost/phpmyadmin> in your browser.
3. Click **Import** in the top menu.
4. Click **Choose File** and select `scc_records_management.sql`.
5. Scroll down and click **Import** (the database `scc_records_management` is created automatically; you do not need to create it first).
6. You should see a green success message and the new database in the left list.

**Option B - command line**

```
"C:\xampp\mysql\bin\mysql.exe" -u root < scc_records_management.sql
```

> **Warning:** importing the file again **deletes the old tables and all their data** and creates them fresh.

**What the file creates**

| Table | Purpose |
|-------|---------|
| `accounts` | Login accounts (BCrypt password hash). The `admin` account is added for you. |
| `password_resets` | Retained for future use; the current app directs forgotten-password requests to the administrator. |
| `departments` | Courses/departments. 4 sample departments are added. |
| `students` | Student records. Linked to `departments`. |
| `officers` | Officer details linked to an Officer-role account and a department. |
| `face_images` | The 5 face pictures of each student (saved as JPEG data). Linked to `students` with `ON DELETE CASCADE`. |
| `attendance_events` | Event dates, department scope, log-out requirement, and default morning/afternoon times. |
| `event_sessions` | One row per enabled morning/afternoon session on each event date. |
| `event_attendance` | One row per student/session, with `time_in` and optional `time_out`; a unique key prevents duplicates. |
| `obligation_adjustments` | Event-specific point deductions/clearings with reason, time, and account history. |

Deleting a student cascades their face and event attendance records, and deleting an event cascades its sessions, attendance, and event-specific obligation history. A department that still has students or officers cannot be deleted.

## 8. How to Configure the Database Connection

The connection string is in **`SCCRecordsManagementSystem/Database.cs`**:

```csharp
private const string connectionString =
    "Server=localhost;Port=3306;Database=scc_records_management;Uid=root;Pwd=;";
```

Change `Server`, `Port`, `Uid`, or `Pwd` only if your MySQL uses different settings
(for example, if you set a root password in XAMPP, write it after `Pwd=`).

## 9. NuGet Packages and Exact Versions

The project uses **only these 4 packages**. Use exactly these versions:

| Package | Version | Used for |
|---------|---------|----------|
| `BCrypt.Net-Next` | **4.2.0** | Hashing passwords |
| `MySql.Data` | **6.9.12** | Connecting to MySQL/MariaDB |
| `OpenCvSharp4` | **4.11.0.20250507** | Webcam, face detection, and face recognition |
| `OpenCvSharp4.runtime.win` | **4.11.0.20250507** | The Windows native OpenCV files that `OpenCvSharp4` needs |

The versions are already written in `SCCRecordsManagementSystem.csproj`, so Visual Studio **restores them automatically** the first time you
open and build the solution (internet connection needed). If a package is missing, install it manually:

**Using the Visual Studio menus**

1. Open the solution (`SCCRecordsManagementSystem.sln`) in Visual Studio.
2. Go to **Tools**.
3. Select **NuGet Package Manager**.
4. Select **Manage NuGet Packages for Solution...**.
5. Open the **Browse** tab.
6. Search for the package name (for example `OpenCvSharp4`).
7. Select the package, then choose the documented version in the **Version** box on the right.
8. Tick the **SCCRecordsManagementSystem** project.
9. Click **Install**.
10. Accept any license prompts. Repeat for the other packages.

**Using the Package Manager Console** (Tools > NuGet Package Manager > Package Manager Console)

```
Install-Package BCrypt.Net-Next -Version 4.2.0 -ProjectName SCCRecordsManagementSystem
Install-Package MySql.Data -Version 6.9.12 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4 -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4.runtime.win -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
```

To restore everything at once: right-click the solution in **Solution Explorer** and choose **Restore NuGet Packages**.

Besides the packages, the project references the built-in .NET Framework library `System.Windows.Forms.DataVisualization` (for the dashboard chart).
It needs no installation.

## 10. Webcam Requirements

- Any built-in or USB webcam that Windows can use (640x480 or better). The program tries camera 0, then 1, then 2.
- Close other programs that use the camera (Zoom, Teams, Windows Camera app, browser tabs) before starting.
- Allow camera access: **Windows Settings > Privacy & security > Camera**, turn on **Camera access** and **Let desktop apps access your camera**.
- For good results, use **good, even lighting** (a window or lamp in front of the face, not behind it), and keep the face about 40-70 cm from the camera.

## 11. How to Build the Project

1. Open `SCCRecordsManagementSystem/SCCRecordsManagementSystem.sln` in Visual Studio.
2. Wait for NuGet to restore the packages (see the status bar at the bottom).
3. Choose **Debug** and **Any CPU** in the top toolbar.
4. Click **Build > Build Solution** (or press **Ctrl+Shift+B**).
5. The **Output** window should end with `Build: 1 succeeded, 0 failed`.

The build copies the face detector file `Data\haarcascade_frontalface_default.xml` and the OpenCV `dll` folder next to the `.exe`
(`SCCRecordsManagementSystem\bin\Debug\net472\`). Do not delete them.

## 12. How to Run the Project

1. Make sure XAMPP **MySQL is running** and the SQL file was imported.
2. In Visual Studio press **F5** (or click **Start**).
3. Log in with **admin / admin123**.
4. Use the left menu to move between Dashboard, Student Records, Departments, Face Recognition, Attendance Records, Event Management, Student Obligations, and Reports & Analysis. Admins also see Officer Records.

If the database is not running, the program shows a clear message instead of crashing.

## 13. How to Register a Student

1. Click **Student Records** in the left menu, then **ADD STUDENT**.
2. Fill in the information. Fields marked with `*` are required:
   ID Number, First Name, Last Name, Course, Year Level, Gender, and Status.
   Middle Name, Email, and Contact Number are optional but are checked when typed (valid email, valid contact number).
3. Capture the 5 face pictures (next section).
4. Click **SAVE STUDENT**.

The system will refuse to save when:

- a required field is empty or invalid,
- the **ID Number** or **Email** already belongs to another student (the same full name asks you to confirm),
- fewer than 5 valid face pictures were captured.

**Other student actions** (Student Records page)

| Action | How |
|--------|-----|
| View | Click a row. The details and photo appear on the left. |
| Search / filter | Type in **Search** (name or ID number), or use the **Course**, **Year Level**, and **Face** filters. Click **CLEAR FILTERS** to reset. |
| Edit | Select a row and click **EDIT STUDENT** (or double-click the row). |
| Delete | Select a row and click **DELETE STUDENT**, then confirm. The student's face pictures and attendance records are deleted too. |

**Re-registering faces:** when editing, tick **Re-register face images (capture 5 new pictures)**.
The old pictures are kept unless you tick this box. Once ticked, the new set of 5 pictures replaces the old set only when you save.

## 14. How to Capture the Required 5 Face Images

On the **Student Registration** page:

1. Click **START CAMERA** (it also starts by itself). The live preview appears.
2. Look at the camera. A **green box** around your face means the picture is good. A **red box** means a problem, and the message
   under the preview tells you what to fix (for example *"No face detected"*, *"Multiple faces detected"*,
   *"Your face is too small. Please move closer to the camera."*, *"The picture is too dark. Please turn on more light."*).
3. Click **CAPTURE FACE**. The picture appears in the next thumbnail and the progress changes to
   **"Face 1 of 5 captured"**, then 2 of 5, up to **"Face 5 of 5 captured"**.
4. Follow the hints under the thumbnails to get slightly different pictures:
   1. Look straight, 2. Slightly left, 3. Slightly right, 4. Slightly up, 5. Normal or smile.
5. After the 5th picture, click **SAVE STUDENT**.

Rules the program enforces:

- **Exactly one** face must be in the picture. A picture with no face or with two or more faces is **rejected**.
- Faces that are too small, too dark, too bright, blurry, or cut by the edge of the picture are rejected.
- **RETAKE ALL FACES** clears the 5 pictures so you can start again.
- The student can only be saved when all 5 pictures are captured.

## 15. How Face Recognition Works

The program uses two simple, classic OpenCV methods (no internet and no cloud service needed):

1. **Detect** - a *Haar cascade* (`Data\haarcascade_frontalface_default.xml`) finds where faces are in the camera picture.
2. **Check the quality** - the face must be big enough, not at the edge of the picture, not too dark or too bright, and not blurry.
3. **Prepare** - the face is cut out, turned to gray, resized to 100x100, and its contrast is balanced.
4. **Recognize** - an *LBPH face recognizer* is trained from the 5 saved pictures of every student when the Face Recognition page opens.
   For a new face it returns the closest student and a **distance** number. **A smaller distance means a closer match.**
5. **Decide** - the face is accepted as that student only if the distance is **70 or lower**. Otherwise it is an *Unknown face*.
6. **Make sure** - the same student must be recognized in **3 frames in a row** before attendance is recorded. This avoids wrong or accidental matches.

Using 5 pictures per student (different small head angles) makes the recognition more reliable than using only 1 picture.

The numbers (distance 70, 3 frames, minimum face size 90 pixels) are constants at the top of `Services\FaceService.cs` and
`FaceRecognition.cs`. If your webcam gives too many *Unknown face* results, you can raise the distance a little (for example 80); if it
recognizes the wrong people, lower it (for example 60).

## 16. How to Record Attendance

1. In **Event Management**, create an event with a start and end date, choose a department or **All Departments**, and enable Morning and/or Afternoon. Set each session's start and end times. For a 2-day event, the chosen sessions are generated for both days.
2. Open **Face Recognition during one of the scheduled session times**. The session selector lists event sessions currently in progress.
3. Choose **Log In** when students arrive. The webcam starts; the student stands in front of it and looks at the camera.
4. When a registered student is recognized, the screen shows their name and the successful log-in time.

   ```
   Student Recognized
   Name: Juan Dela Cruz
   Student ID: 2026-001
   Event Session: Campus Clean-up - Nov 18 - Morning
   Status: Present
   Log In: 7:53 AM
   Log In successfully recorded.
   ```

5. To record a departure time, choose **Log Out** and scan the student again before the session ends. If **Require students to log out** was selected for the event, explain that to attendees. Logging out is still optional for a session's Present status; the setting is for event instructions and the UI.
6. Click **NEXT STUDENT** to clear the screen for the next person. Each student can log in only once and log out only once per session.

What the screen tells you in other situations:

| Situation | Message |
|-----------|---------|
| Login already recorded in the session | *Already logged in at 7:53 AM.* (no duplicate record is created) |
| Logout already recorded | The existing logout time is shown; no duplicate is created. |
| Log out without logging in first | The system explains that the student must log in first. |
| Scan outside the scheduled session | Attendance is refused; open the scan screen during the session time. |
| Student is not included in the event department | Attendance is refused and no record is created. |
| No event session is currently open | The screen asks the operator to create or wait for an active session. |
| Face not registered | *Unknown face. This person is not registered in the system.* |
| No face in front of the camera | *No face detected. Please stand in front of the camera.* |
| Two or more people | *Multiple faces detected. Only one student can stand in front of the camera.* |
| Face too small/dark/blurry | A message telling what to fix |
| Student set to **Inactive** | *... was recognized, but is not an active student.* Attendance is not recorded. |
| No student has registered faces yet | *No student has a registered face yet. Please register a student first.* |
| Webcam unplugged or in use | *The webcam was disconnected...* or *No webcam was found or it cannot be opened.* Then click **RESTART CAMERA**. |
| Database stopped | A database message is shown on the screen; recording continues once MySQL is back |

Click **RESTART CAMERA** after a student was added or re-registered, or after the webcam was reconnected. It also reloads the saved faces. Every morning and afternoon block is a separate attendance session, so students need to log in again when the next session starts.

## 17. How to View Attendance Records

1. Click **Attendance Records** in the left menu.
2. Use the filters:
   - **Event** and **Session** choose one event/session or all matching sessions.
   - **Date** optionally limits the table to one date.
   - **Department**, **Year**, **Status**, and **Search** narrow the results.
3. The table shows event, date, session, student, time in, time out, Present/Absent, and whether required logout is Pending, Complete, or Missing.
4. An absent row is produced only after the session end time. A student registered after the session date, inactive students, and students outside the event's department scope are not counted absent.
5. The **Dashboard** summarizes the current active session. The **Reports & Analysis** page provides date range and department filters, attendance rates, absence points, a most-absent list, and CSV export.

### Event Management

Create, search, edit, or delete events from the left menu. Each event has a date range, venue, participating department or All Departments, and an optional log-out requirement. Morning and afternoon sessions can be toggled independently and their times can be changed. All enabled sessions are created for every date in the range. After attendance, obligation adjustments, or completed sessions exist, only the event name and venue can be edited so historical attendance and absence calculations remain consistent. Create a replacement event or delete the event after confirming its records should be removed if the schedule must change.

### Officer Records and login roles

Only an Admin sees **Officer Records** and can create, edit, delete, or reset an officer's login. Officer details include officer number, name, department, year level, gender, email, contact, position, and school year. The officer username and password are created by the admin. Officers use the same login screen; they cannot open Officer Records. A forgotten password must be reset by the administrator.

### Student Obligations

Each absent student-session adds **10 points**. The Absences count is derived from completed event sessions; it is not erased when points are adjusted. Select one event to deduct points or clear a balance to zero. Every adjustment requires a reason and is stored in the history. The All Events filter is for viewing totals; adjustments require one selected event so the ledger remains attributable.

## 18. Common Errors and Troubleshooting

| Problem | What to do |
|---------|------------|
| *"Cannot connect to the database"* | Start **MySQL** in the XAMPP Control Panel. Check that the connection string in `Database.cs` matches your MySQL port. |
| *"The database username or password is wrong"* | Fix `Uid` and `Pwd` in the connection string (section 8). |
| *"Cannot connect to the database"* even though MySQL is running, and you use **MySQL 8** | Use XAMPP's MariaDB (or MySQL 5.7). `MySql.Data 6.9.12` cannot use MySQL 8's default login method. |
| *"The database 'scc_records_management' was not found"* or *"A database table is missing"* | Import the SQL file (section 7). |
| Wrong username or password | Default is `admin` / `admin123`. An officer must ask the admin to reset their password. If the admin password is lost, re-importing the SQL file restores the default but erases all data. |
| NuGet packages are missing or red in **References** | Right-click the solution > **Restore NuGet Packages**, or install them as in section 9. |
| *"The camera library could not be loaded"* or `DllNotFoundException` for `OpenCvSharpExtern` | Install the **Microsoft Visual C++ Redistributable 2015-2022 (x64)** (section 4). Check that the `dll` folder is next to the `.exe`. Do not change the platform to x86. |
| *"No webcam was found or it cannot be opened"* | Plug in the webcam, close Zoom/Teams/Camera/browser tabs that use it, and allow camera access in **Windows Settings > Privacy & security > Camera**. Then click **START CAMERA** / **RESTART CAMERA**. |
| Black camera picture | Another program is using the camera, or the lens cover is closed. Restart the camera. |
| *"The face detection file was not found"* | Make sure `Data\haarcascade_frontalface_default.xml` exists next to the `.exe`. Rebuild the solution. |
| *No face detected* while a face is visible | Face the camera directly, remove hats/masks, add light in front of the face, and move closer (face at least about 90 pixels wide). |
| *Multiple faces detected* | Only one person may be in front of the camera. Check for posters or photos behind you. |
| A registered student is shown as *Unknown face* | Register the face again in the same lighting where attendance is taken (**Edit Student > Re-register**), or raise the distance limit slightly (section 15). |
| The wrong student is recognized | Re-register both students with clear pictures, or lower the distance limit (section 15). |
| *"Another student already has the ID number ..."* / *"...already uses this email address"* | ID numbers and emails must be unique. Search the list first. |
| *Cannot delete the department* | Move or delete its students first. |
| Build error about `System.Windows.Forms.DataVisualization` | Install the ".NET Framework 4.7.2 targeting pack" and the ".NET desktop development" workload in the Visual Studio Installer. |

## 19. Project Structure

```
SCC_RecordsManagementSystem/
|-- README.md
|-- scc_records_management.sql            <- import this into MySQL/MariaDB
|-- Sample Code/                          <- style reference (not part of the build)
`-- SCCRecordsManagementSystem/
    |-- Database.cs                       <- connection + simple helper methods (like the sample code)
    |-- Models/                           <- Student, Department, face and session attendance results
    |-- Services/                         <- student, event/session attendance, event, officer, obligation,
    |                                        report, department, account and face logic
    |-- Helpers/                          <- Validator, CameraHelper, ImageHelper, NavigationHelper
    |-- Data/haarcascade_frontalface_default.xml   <- face detector file
    |-- LoginForm / CreateAccount / ForgotPassword
    |-- Dashboard, StudentRecords, Registration
    |-- FaceRecognition, AttendanceMonitoring, EventManagement
    |-- OfficerRecords, Obligations, ReportsAndAnalysis
    `-- SidebarMenu, WindowButtons        <- shared menu and window buttons
```

How the code is divided (each part is small and simple):

- **Database** - `Database.cs` opens connections, runs queries with `Dictionary<string, object>` parameters, and turns database errors into friendly messages.
- **Student operations** - `StudentService` (add, edit, delete, search, duplicate checks, saving the 5 faces).
- **Face detection / recognition** - `FaceService` (detect, quality check, train, recognize) and `CameraHelper` (webcam).
- **Event and session logic** - `EventService` generates daily sessions for each event date; `AttendanceService` enforces one login and one optional logout per student/session and calculates completed-session absences.
- **Officer accounts** - `OfficerService` restricts account creation, edits and deletion to admins.
- **Obligations and reporting** - `ObligationService` calculates 10 points per completed absent session and stores reasoned adjustments; `ReportService` builds session/department summaries for display and CSV export.
- **Validation** - `Validator` (required fields, email, contact number, ID number).
- **UI** - the `.cs` form files only react to buttons and show messages; the `.Designer.cs` files hold the generated layout.

## 20. Known Limitations

- The face recognition is simple on purpose (Haar + LBPH). It works best in steady lighting and when students look at the camera.
  It is not designed to stop someone holding up a photo of a student.
- Officer accounts are managed by an Admin. The app does not provide email-based password recovery; forgotten passwords must be reset by an Admin from Officer Records.
- The connection string (including the empty root password) is stored in the source code, which is fine for this project.
- Face recognition is a classroom-level Haar/LBPH system and is not spoof-proof; its thresholds should be checked with the actual webcam and lighting.
- Attendance can only be recorded during the configured session times. A required logout that is missed is reported as Missing, but a successful login remains Present.
