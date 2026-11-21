# SCC Records Management System with Face Recognition Attendance

A **C# Windows Forms** student project for managing students, departments, officers and
school events. Attendance is recorded with **face recognition** using the computer's webcam.
All data is saved in a **MySQL / MariaDB** database.

The code is written in a simple, beginner style. It follows the example in
`Sample Code/ConnectDatabase` (a `ConnectDB` database class and simple model classes like `PersonalInfo`).

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Main Features](#2-main-features)
3. [System Requirements](#3-system-requirements)
4. [Required Software and Versions](#4-required-software-and-versions)
5. [Visual Studio Requirements](#5-visual-studio-requirements)
6. [Database Setup](#6-database-setup)
7. [How to Import the SQL File](#7-how-to-import-the-sql-file)
8. [How to Configure the Database Connection](#8-how-to-configure-the-database-connection)
9. [Required NuGet Packages](#9-required-nuget-packages)
10. [Webcam Requirements](#10-webcam-requirements)
11. [How to Build the Project](#11-how-to-build-the-project)
12. [How to Run the Project](#12-how-to-run-the-project)
13. [How to Register a Student](#13-how-to-register-a-student)
14. [How to Capture the 5 Face Pictures](#14-how-to-capture-the-5-face-pictures)
15. [How Face Recognition Works](#15-how-face-recognition-works)
16. [How to Record Attendance](#16-how-to-record-attendance)
17. [How to View Attendance Records](#17-how-to-view-attendance-records)
18. [The Other Menus](#18-the-other-menus)
19. [Project Files](#19-project-files)
20. [Common Errors and Troubleshooting](#20-common-errors-and-troubleshooting)

---

## 1. Project Overview

| Item | Value |
|---|---|
| Language | C# 7.3 |
| Project type | Windows Forms App (**.NET Framework 4.7.2**) |
| Database | MySQL / MariaDB (tested with **XAMPP MariaDB 10.4**) |
| Database name | `scc_management` |
| SQL file | `scc_records_management (1).sql` (in the repository root folder) |
| Solution file | `SCCRecordsManagementSystem/SCCRecordsManagementSystem.sln` |
| Face detection | Haar Cascade (`haarcascade_frontalface_default.xml`) from OpenCV |
| Face recognition | LBPH face recognizer from OpenCV (through the **OpenCvSharp4** NuGet package) |
| Starter login | Username **admin**, password **admin123** |

Attendance is **based on events**. An event can last one or more days, and every day can
have a **Morning**, **Afternoon** and/or **Evening** session. Every session is a separate
attendance. Students scan their face to **Log In**, and if the event needs it, they scan again to **Log Out**.

---

## 2. Main Features

**Login and accounts**
- Log in as **Admin** or **Officer** (passwords are hashed with BCrypt).
- Create a new Admin account (an existing Admin must type the Admin password).
- Forgot password / reset password (an Admin must type the Admin password).
- Every log in and log out is saved in the **Login History** (Admin only).

**Dashboard**
- Total students (active and face registered), total officers (and admin accounts),
  total events (ongoing and upcoming).
- Today's attendance overview: present, absent and pending, with a pie chart.
- Recent attendance activities (the last 15 scans), a live date and time, and a **Refresh** button.
- **Login History** button (Admin only).

**Departments**
- Add, edit, delete and search departments (courses).
- A department that still has students, officers or events cannot be deleted.
- **View Students** opens Student Records filtered to that department.

**Student Records**
- Add, view, edit, delete, search and filter students (department, year level, face status).
- Duplicate prevention for student number and email.
- Student details with photo, attendance summary and obligation balance.

**Student Face Registration**
- A new student **must** have exactly **5 valid face pictures** before he/she can be saved.
- Live webcam preview with a colored box around the face and helpful tips.
- Only pictures with **exactly one clear face** are accepted (not too dark, not too bright, not blurry, not too far).
- **Retake All Faces** to start over. Faces can be re-registered when editing a student.
- Warns you when the new face looks like another registered student.

**Face Recognition (attendance)**
- The webcam starts automatically. The open session is selected automatically.
- Choose **Log In** or **Log Out**.
- Recognizes the student, shows the student card and saves the attendance automatically.
- Duplicate attendance is blocked: *"Attendance already recorded at 7:53 AM."*
- Re-checks that the student is active, has `face_registered = 1` and still has exactly five
  saved face pictures, and is on the selected event roster immediately before recording.
  The student row is locked during this database transaction so registration changes cannot
  race the final attendance check.
- Clear messages for: no face, more than one face, unknown face, webcam unavailable or
  unplugged, no open session, no registered faces, and database errors.

**Event Management**
- Add, edit and delete events (one day or several days, at most 31 days).
- Choose the sessions (Morning, Afternoon, Evening) and their times.
- Choose the department and year level that must attend (or all).
- Option: **Log out Needed** (students must also scan when leaving).
- See the **Sessions** and the **Students (Roster)** of each event with their attendance.

**Attendance Monitoring**
- View all attendance records per event, session, date, department and year level.
- Search by student number or name. Filter by status: Present, Absent, Pending.
- Attendance count and attendance rate.
- Manual entry when the camera cannot recognize a student: **Mark Present**, **Mark Log Out**, **Remove**.
- **Export CSV** (opens in Excel).

**Reports and Analysis**
- 7 report types: Attendance per Session, Attendance per Department, Student Attendance Summary,
  Most Absent Students, Missing Log Out, Detailed Attendance List and Obligation Balances.
- Chart of present/absent per department. Export to CSV.

**Obligations**
- Every **absence = 10 points**. The absent count and the balance of every student are shown.
- **Add Points**, **Deduct Points** or **Clear to 0** (every change needs a reason and is saved).
- Search and filters (department, year level, with balance, cleared, with absences).
- **View Details** shows every absence and every adjustment of a student.

**Officer Records**
- Officers log in with their own username and password and use the same menus as the Admin,
  except the menus the Admin did not allow in **Rules and Permission**.
- **Only the Admin** can add, edit and delete officers and create their username and password.
- Officers have the same information as students (ID number, name, course, year level, and so on).
  **Find Student** copies the details of an existing student.
- Search and filters (course, position, school year), and last log in.

**Rules and Permission** (Admin only)
- Add, edit, delete, search and activate or deactivate rules.
- Choose which menus the **Officer** role may open.

---

## 3. System Requirements

| Requirement | Minimum |
|---|---|
| Operating system | Windows 10 or Windows 11 (64-bit recommended) |
| RAM | 4 GB (8 GB recommended) |
| Webcam | Built-in laptop camera or USB webcam |
| Disk space | About 1 GB for the project, packages and database |
| Internet | Only needed the first time, to download the NuGet packages |

---

## 4. Required Software and Versions

| Software | Version | Download |
|---|---|---|
| Visual Studio Community | 2019 or 2022 | https://visualstudio.microsoft.com/ |
| .NET Framework | 4.7.2 or newer (already part of Windows 10/11) | |
| XAMPP (Apache + MySQL/MariaDB + phpMyAdmin) | 7.4 / 8.x (MariaDB 10.4 or newer) | https://www.apachefriends.org/ |
| Microsoft Visual C++ Redistributable | 2015-2022 (x64 **and** x86) | https://learn.microsoft.com/cpp/windows/latest-supported-vc-redist |

> You may use **MySQL 8.0.16+** instead of XAMPP. See the MySQL 8 note in
> [Troubleshooting](#20-common-errors-and-troubleshooting).

---

## 5. Visual Studio Requirements

When installing Visual Studio, open the **Visual Studio Installer** and select:

1. Workload: **.NET desktop development**.
2. In **Individual components**, make sure **.NET Framework 4.7.2 targeting pack** is checked.
3. Also keep **NuGet package manager** checked (it is checked by default).

---

## 6. Database Setup

1. Install **XAMPP**.
2. Open the **XAMPP Control Panel**.
3. Click **Start** beside **Apache** and **MySQL**. Both should turn green.
4. Click **Admin** beside **MySQL**. **phpMyAdmin** opens in your browser
   (or go to http://localhost/phpmyadmin).

---

## 7. How to Import the SQL File

The SQL file creates the database **`scc_management`** by itself, with all tables, views,
starter departments, starter rules, permissions and the Admin account.

1. Open **phpMyAdmin** (http://localhost/phpmyadmin).
2. Click the **Import** tab at the top (you do not need to create a database first).
3. Click **Choose File** and select `scc_records_management (1).sql` from the repository root folder.
4. Leave the other settings as they are and click **Import** (or **Go**) at the bottom.
5. Wait for the green message *"Import has been successfully finished"*.
6. On the left side you should now see the database **scc_management** with 14 tables and 2 views.

> **Warning:** importing the file again **deletes all saved records** (students, faces, attendance...).
> Use **Export** in phpMyAdmin first if you want a backup.

**What the database contains**

| Table / View | What it stores |
|---|---|
| `accounts` | Usernames, BCrypt passwords and the role (Admin or Officer) |
| `departments` | Courses (BSIT, BSBA, BEED, BSED by default) |
| `students` | Student information, status and `face_registered` (1 = all 5 faces saved) |
| `face_images` | The 5 face pictures of each student (160 x 160 PNG, numbers 1 to 5) |
| `officers` | Officer information linked to one `accounts` row |
| `events` | Event name, venue, dates, sessions, department/year level, log out needed |
| `event_sessions` | One row for every Morning / Afternoon / Evening of every event day |
| `event_students` | The students required to attend each event (the roster) |
| `event_attendance` | Time in and time out of a student in a session |
| `obligation_adjustments` | Points added (+) or deducted (-) by an Admin/Officer, with the reason |
| `login_history` | Every log in and log out |
| `rules`, `role_permissions` | Rules and the menus that each role may open |
| `password_resets` | Kept from the original database (reserved) |
| `attendance_records` (VIEW) | Status of every student in every session: Present, Absent or Pending |
| `obligation_summary` (VIEW) | Absent count x 10 + adjustments = balance (never below 0) |

**Relationships:** deleting a student also deletes his/her face pictures, attendance and
obligation adjustments (`ON DELETE CASCADE`). Deleting an event deletes its sessions, roster and
attendance. Deleting an officer's account deletes the officer. A department that is still used
cannot be deleted (`ON DELETE RESTRICT`).

---

## 8. How to Configure the Database Connection

The connection string is in **`SCCRecordsManagementSystem/Database.cs`** (line 16):

```csharp
private static string connectionString = "Server=localhost;Database=scc_management;Uid=root;Pwd=;";
```

The default XAMPP account is user `root` with **no password**, so it works without changes.
If your MySQL has a password, a different user or a different port, change it, for example:

```csharp
private static string connectionString = "Server=localhost;Port=3307;Database=scc_management;Uid=root;Pwd=mypassword;";
```

---

## 9. Required NuGet Packages

These are the **only** NuGet packages used by the project (they are already listed in
`SCCRecordsManagementSystem/packages.config`):

| Package | Version | Used for |
|---|---|---|
| **MySql.Data** | **6.9.12** | Connecting to MySQL / MariaDB |
| **BCrypt.Net-Next** | **4.2.0** | Hashing and checking passwords |
| **OpenCvSharp4** | **4.11.0.20250507** | Webcam, face detection and LBPH face recognition |
| **OpenCvSharp4.runtime.win** | **4.11.0.20250507** | The native OpenCV files for Windows (`OpenCvSharpExtern.dll`) |

These 4 packages are installed **automatically** as dependencies of OpenCvSharp4
(do not install them by hand): `System.Buffers 4.6.1`, `System.Memory 4.6.3`,
`System.Numerics.Vectors 4.6.1`, `System.Runtime.CompilerServices.Unsafe 6.1.2`.

The chart in the Dashboard and Reports uses `System.Windows.Forms.DataVisualization`,
which is already part of .NET Framework (no NuGet package needed).

### Option A - Restore (easiest)

Because the packages are already listed in the project, Visual Studio can download them for you:

1. Open the solution in Visual Studio.
2. In **Solution Explorer**, right-click the **Solution** (the top item).
3. Click **Restore NuGet Packages**.
4. Wait until the status bar says *"Restore completed"*.

### Option B - Install with the NuGet Package Manager window

Use this if restore did not work or if a package is missing:

1. Open the solution in Visual Studio.
2. Go to **Tools**.
3. Select **NuGet Package Manager**.
4. Select **Manage NuGet Packages for Solution...**.
5. Open the **Browse** tab.
6. Search for the package name (for example `MySql.Data`).
7. On the right side, select the version from the table above (for example **6.9.12**).
8. Check the **SCCRecordsManagementSystem** project.
9. Click **Install**.
10. Accept any license prompt (**I Accept**).
11. Repeat steps 6 to 10 for `BCrypt.Net-Next`, `OpenCvSharp4` and `OpenCvSharp4.runtime.win`.

### Option C - Package Manager Console

Go to **Tools > NuGet Package Manager > Package Manager Console** and run:

```powershell
Install-Package MySql.Data -Version 6.9.12 -ProjectName SCCRecordsManagementSystem
Install-Package BCrypt.Net-Next -Version 4.2.0 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4 -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4.runtime.win -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
```

If the packages are listed but the files are missing, this command downloads them again:

```powershell
Update-Package -reinstall -ProjectName SCCRecordsManagementSystem
```

---

## 10. Webcam Requirements

- Any built-in or USB webcam that works in the Windows **Camera** app.
- The program tries camera **0** (built-in) first, then cameras **1** and **2** (USB).
- Only **one program** can use the webcam at a time. Close Zoom, Teams, Messenger, OBS, the Camera app, etc.
- Windows must allow desktop apps to use the camera:
  **Settings > Privacy & security > Camera > Let desktop apps access your camera = On**.
- Use **good lighting** (light in front of the face, not behind) and a plain background.

---

## 11. How to Build the Project

1. Do steps 6 to 8 (database) first.
2. Open `SCCRecordsManagementSystem/SCCRecordsManagementSystem.sln` in Visual Studio.
3. Restore or install the NuGet packages (section 9).
4. At the top toolbar, choose **Debug** and **Any CPU**.
5. Go to **Build > Rebuild Solution**.
6. The **Output** window should say: `Rebuild All: 1 succeeded, 0 failed`.

When the build succeeds, these files are copied to `bin\Debug` automatically:
- `haarcascade_frontalface_default.xml` (the face detector file)
- `dll\x64\OpenCvSharpExtern.dll` and `dll\x86\OpenCvSharpExtern.dll` (native OpenCV)

---

## 12. How to Run the Project

1. Make sure **MySQL** is running in the XAMPP Control Panel.
2. In Visual Studio, press **F5** (or click **Start**).
3. Log in with username **admin** and password **admin123**.
4. **Change the starter password** later with *Forgot Password* (type the Admin password to allow it).

**Suggested first steps**

1. **Departments**: check or add your courses.
2. **Student Records**: add students and register their 5 face pictures.
3. **Event Management**: add an event with the sessions of today.
4. **Face Recognition**: let the students scan their faces.
5. **Attendance Monitoring / Reports and Analysis / Obligations**: view the results.
6. **Officer Records**: create officer accounts (Admin only).

---

## 13. How to Register a Student

1. Click **Student Records** in the left menu.
2. Click **ADD STUDENT**.
3. Fill in the student information. Fields with `*` are required:
   - **Student Number** (3 to 20 letters, numbers or `-`, must be unique), for example `2026-001`
   - **First Name**, **Middle Name** (optional), **Last Name** (letters, spaces, `.`, `'` and `-` only)
   - **Department**, **Year Level**, **Gender**, **Status** (Active / Inactive)
   - **Email** (optional, must be unique) and **Contact Number** (optional, `09XXXXXXXXX` or `+639XXXXXXXXX`)
4. Capture the **5 face pictures** (next section).
5. Click **SAVE STUDENT**. The student is saved only when all 5 faces are captured.

The new student is also added to the roster of every current or upcoming event that his/her
department and year level must attend.

**Edit:** select a student and click **EDIT STUDENT** (or double-click the row). You can keep
the saved faces, or check the face option to capture 5 new pictures (re-register).

**Delete:** select a student, click **DELETE STUDENT** and confirm. His/her face pictures,
attendance and obligation records are deleted too.

---

## 14. How to Capture the 5 Face Pictures

1. In the student registration page, click **START CAMERA**. The live preview appears.
2. Look at the camera. Follow the tip shown for each picture (look straight, turn slightly, and so on).
3. The box around the face shows if the picture is good:
   - **Green** = good, you can capture.
   - **Orange** = something must be fixed (read the message).
4. Click **CAPTURE FACE**. The progress shows *"Face 1 of 5 captured"* ... *"Face 5 of 5 captured"*.
5. A picture is **not accepted** when:
   - no face is detected,
   - more than one face is detected,
   - the face is too small (too far), too dark, too bright or blurry,
   - it is exactly the same as a picture already captured.
6. Click **RETAKE ALL FACES** to delete the captured pictures and start again.
7. After **5 of 5**, click **SAVE STUDENT**.

---

## 15. How Face Recognition Works

The project uses **OpenCV** through the **OpenCvSharp4** C# library (no Python):

1. **Face detection** - the **Haar Cascade** file `haarcascade_frontalface_default.xml` finds
   where the faces are in the camera picture.
2. **Preparation** - every face is cut out, changed to gray, resized to 100 x 100 pixels and its
   light is equalized. The same preparation is used when registering and when recognizing.
3. **Training** - when the Face Recognition page opens, the **LBPH** (Local Binary Patterns
   Histograms) recognizer learns the 5 pictures of every **active** student with **exactly 5** faces.
4. **Recognition** - for every face in the camera, LBPH returns the most similar student and a
   **distance** (smaller = more similar). A face is accepted when the distance is **70 or lower**.
5. **Confirmation** - the same student must be matched in **3 camera pictures in a row** before
   the attendance is saved. After **5 pictures in a row** with no match, the face is called **Unknown**.

Why 5 pictures? Different angles and expressions give the recognizer more examples, so it
recognizes the student more reliably.

You can change the numbers at the top of `FaceRecognition.cs` (`matchThreshold`, `matchesNeeded`, `missesNeeded`).

---

## 16. How to Record Attendance

1. Create an event in **Event Management** (section 18) with a session that is **open now**.
2. Click **Face Recognition** in the left menu. The webcam starts by itself.
3. Select the **session** (only sessions that are open right now are listed) and choose **Log In** or **Log Out**.
4. Students stand in front of the camera **one at a time**.
5. When recognized, the student card shows:

   ```
   Student Recognized
   Name: Juan Dela Cruz
   Student ID: 2026-001
   Status: Present
   Time: 7:53 AM
   Attendance Successfully Recorded
   ```

6. The program waits for the next student. Click **NEXT PERSON** to clear the card right away.
7. Click **RESTART / RELOAD** after registering new students or creating a new event.

**Rules**
- A student can only log in **once per session**. A second scan shows *"Attendance already recorded at 7:53 AM."*
- **Log Out** works only after a Log In, and only once.
- Only students on the event's roster can be recorded.
- When a session ends without a Log In, the student becomes **Absent** (10 obligation points).

| Message | Meaning |
|---|---|
| No face detected | Nobody is in front of the camera |
| More than one face | Only one person at a time |
| Unknown face | The face is not registered, or the picture is not clear |
| No open session | No event session is going on right now |
| Webcam not found / disconnected | Check the cable, privacy settings or other apps |
| Database error | MySQL is not running, or the connection settings are wrong |

---

## 17. How to View Attendance Records

Open **Attendance Monitoring**:

- Choose the **Event**, **Session**, **Date**, **Department** and **Year Level**.
- Search by **student number or name**.
- Check **Present**, **Absent** and/or **Pending** (session not finished yet). **Today** shows today only.
- **Log Out** filter: Logged Out, Waiting, Missing Log Out or Not Required.
- **Clear Filters** shows everything again.
- The summary shows the counts and the attendance rate.
- **Mark Present**, **Mark Log Out** and **Remove** fix records by hand (for example when the camera cannot recognize a student).
- **Export CSV** saves the list so it can be opened in Excel.

Open **Reports and Analysis** for summaries, the department chart and the most absent students.

---

## 18. The Other Menus

**Event Management**
1. Click **+ Add Event**. Enter the name, venue, organizer, start and end date.
2. Check **Morning**, **Afternoon** and/or **Evening** and set their times.
3. Choose who must attend (department and year level, or all).
4. Check **Log out Needed** if students must also scan when they leave.
5. Click **Save Event**. One session is created for every checked session of every day.
6. Search events or filter by status (Upcoming, In Progress, Completed). Double-click an event to edit it.
7. Select an event to see its **Sessions** or **Students (Roster)** below.
   After a session has started, only the name, venue and organizer can be changed.

**Obligations**
- The balance is: **absences x 10 + added points - deducted points** (never below 0).
- Select a student, type the **points** and a **reason**, then click **Add Points**, **Deduct Points** or **Clear to 0**.
- **View Details** (or double-click) shows each absence and each adjustment.
  The Admin can delete a wrong adjustment there.

**Officer Records** (add/edit/delete = Admin only)
1. Click **Add Officer**. Type the ID number and click **Find Student** to copy a student's details, or type them.
2. Choose the **Position** and **School Year**.
3. Create the officer's **Username** and **Password** (at least 6 characters).
4. Click **SAVE OFFICER**. The officer can now log in.

**Rules and Permission** (Admin only)
- **New Rule**, type the description, check **Active**, then click **Add Rule** / **Save Changes**. **Delete Rule** removes it.
- Select the **Officer** role, check the menus officers may open, then click **Save Permissions**.

**Login History** (Admin only, button on the Dashboard)
- Search by username or name, filter by role and date (**Today**, **Last 7 Days**, **Last 30 Days**).

---

## 19. Project Files

```
SCC_RecordsManagementSystem/
|-- README.md
|-- scc_records_management (1).sql      <- import this in phpMyAdmin
|-- Sample Code/ConnectDatabase/        <- the coding style reference
`-- SCCRecordsManagementSystem/
    |-- SCCRecordsManagementSystem.sln  <- open this in Visual Studio
    |-- Database.cs                     <- connection string and shared database methods
    |-- AccountInfo.cs, StudentInfo.cs, OfficerInfo.cs, EventInfo.cs   <- model classes
    |-- haarcascade_frontalface_default.xml   <- face detector file
    `-- (the forms below)
```

| Form | File | What it does |
|---|---|---|
| Login | `Form1.cs` (class `lnkCreateAccount`) | Log in, opens Create Account and Forgot Password |
| Create Account | `CreateAccount.cs` | New Admin account (needs the Admin password) |
| Forgot Password | `ForgotPassword.cs` | Reset a password (needs the Admin password) |
| Dashboard | `Dashboard.cs` | Totals, today's chart, recent scans, clock |
| Login History | `LoginHistory.cs` | Log in / log out list (Admin only) |
| Departments | `Departments.cs`, `AddDepartment.cs` | Department CRUD |
| Student Records | `StudentRecords.cs` | Student list, search, filters, details, delete |
| Registration | `Registration.cs` | Add / edit student and capture 5 faces |
| Face Recognition | `FaceRecognition.cs` | Webcam attendance |
| Event Management | `EventManagement.cs`, `AddEvent.cs` | Event CRUD, sessions and roster |
| Attendance Monitoring | `AttendanceMonitoring.cs` | Attendance records, filters, manual entry, CSV |
| Reports and Analysis | `ReportsAndAnalysis.cs` | Reports, chart, CSV |
| Obligations | `Obligations.cs`, `ObligationDetails.cs` | Points, adjustments, details |
| Officer Records | `OfficerRecords.cs`, `AddOfficer.cs` | Officer CRUD and accounts |
| Rules and Permission | `RulesPermission.cs` | Rules CRUD and role permissions |

Edit layouts and controls in each form's `*.Designer.cs` file. For example, the
Departments and Event Management grid columns (including **Edit** / **Delete**) are
defined there, so they can be edited in Visual Studio's Designer. Their
`DataPropertyName` values match the SQL column names. The corresponding `.cs` files
contain the data loading and click handlers; refreshing or filtering does not recreate
the columns or change their order.

**Useful methods in `Database.cs`** (like `ConnectDB` in the sample code):
`GetConnection()`, `saveUpdateDeleteData()`, `GetData()`, `GetValue()`, `GetCount()`,
`searchAndGetStudentInfo()`, `searchAndGetOfficerInfo()`, `searchAndGetEventInfo()`,
`loadDepartmentComboBox()`, `isAllowed()`, `showError()`.

---

## 20. Common Errors and Troubleshooting

| Problem | Solution |
|---|---|
| *"Cannot connect to the database"* | Start **MySQL** in XAMPP. Check the connection string in `Database.cs`. |
| *"Unknown database 'scc_management'"* | Import `scc_records_management (1).sql` (section 7). |
| MySQL in XAMPP does not start (port 3306 is used) | Another MySQL is running. Stop it, or change the port in XAMPP and add `Port=...;` to the connection string. |
| *"Authentication method 'caching_sha2_password' not supported"* (MySQL 8) | Run in MySQL: `ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY '';` (put your password between the quotes). |
| SQL import error on `CHECK` | Use XAMPP with MariaDB 10.4+ or MySQL 8.0.16+. Older versions are not supported. |
| Build error: *"This project references NuGet package(s) that are missing"* | Right-click the Solution > **Restore NuGet Packages**, then **Rebuild**. |
| Build error: *"The type or namespace name 'MySql' / 'OpenCvSharp' / 'BCrypt' could not be found"* | The package is not installed. Install it with the exact version from section 9. |
| *"Unable to load DLL 'OpenCvSharpExtern'"* | Install the **Visual C++ Redistributable 2015-2022** (x64 and x86). Reinstall `OpenCvSharp4.runtime.win 4.11.0.20250507`. Rebuild and check that `bin\Debug\dll\x64\OpenCvSharpExtern.dll` exists. |
| *"Face detector file is missing"* | `haarcascade_frontalface_default.xml` must be in `bin\Debug`. In Solution Explorer, select the file > Properties > **Copy to Output Directory = Copy if newer**. Rebuild. |
| *"No webcam found"* | Plug in the webcam, close other apps using it, turn on camera access for desktop apps in Windows Privacy settings. |
| Webcam picture is black | Another app is using the camera, or the camera cover/switch is closed. |
| A registration capture is rejected after a camera read fails | Wait for a new valid webcam frame. The last displayed picture is not reused as a new capture. |
| *"No face detected"* | Move closer, face the camera, improve the light. |
| *"More than one face"* | Only one person in front of the camera. |
| Student is always *"Unknown"* | Check that the student is **Active**, has **5 faces**, and is on the event roster. Click **RESTART / RELOAD**. Re-register the faces in good light. |
| Wrong student is recognized | Re-register both students' faces in good light. You can lower `matchThreshold` in `FaceRecognition.cs` (for example 60). |
| *"No open session"* in Face Recognition | The current time must be between the start and end of a session. Check the event dates/times and the computer's clock. |
| *"Attendance already recorded"* | This is correct: a student can only log in once per session. |
| Face registration is reported incomplete during attendance | Open Student Records, edit the student, and re-register all five face pictures. Attendance is not recorded unless the student is active, marked as face registered, and has exactly five saved pictures. |
| *"Access Denied"* when opening a menu | Officers can only open the menus allowed in **Rules and Permission**. Log in as Admin to change them. |
| Forgot the Admin password | In phpMyAdmin, open `scc_management` > SQL and run the query below. The password becomes **admin123**. |

```sql
UPDATE accounts
SET password = '$2a$11$ImieFcUoeFA4gmZBkIB1m.TAvs7tQi5eExMCNZ0s4ZV0CcaFKxmfa'
WHERE username = 'admin';
```
