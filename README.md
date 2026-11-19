# SCC Records Management and Face Attendance

C# Windows Forms student project for managing students, departments, officers,
multi-day events, face-recognition attendance, absence obligations, and reports.

## 1. This branch preserves the original project

`feature/preserve-original-forms` starts from `main`, not from the previous face-recognition branch.
All original forms, designer files, resources, class names, sample code, and the original SQL filename remain.
The existing blue sidebar is still part of **each individual form**, not a separate UserControl.
The original absolute-positioned layouts use fixed sizing to keep added controls aligned.

Application logic is in the existing form code:

| Original file | What it does |
|---|---|
| `Form1.cs` (`lnkCreateAccount`) | Admin/officer login and login history |
| `CreateAccount.cs`, `ForgotPassword.cs` | Admin-created administrator accounts and admin-only password resets |
| `Dashboard.cs` | Counts, selectable session chart, recent attendance and clock |
| `StudentRecords.cs`, `Registration.cs` | Search, details with attendance summary, create/edit/delete, five-picture webcam registration |
| `Departments.cs`, `AddDepartment.cs` | Department CRUD |
| `Event Management.cs`, `AddEvent.cs` | Event CRUD, morning/afternoon schedules, saved participant roster |
| `FaceRecognition.cs` | Webcam detection, recognition, session login/logout |
| `AttendanceMonitoring.cs` | Event/session attendance history and filtering |
| `Officerr Records.cs` | Officer CRUD and officer account/password management |
| `Obligations.cs` | Ten points per absent session, deductions, clearing, adjustment history, CSV |
| `ReportsAndAnalysis.cs` | Session totals, student attendance, missing logout, most-absent students, CSV |
| `RulesPermission.cs`, `LoginHistory.cs` | Informational rules, officer module permissions, account management, login audit |

`Database.cs` follows the sample's `MySqlConnection`, `MySqlCommand`,
`DataTable`, and `Dictionary<string, object>` approach.
`Program.cs` contains only the entry point and small common window/control operations.
There are **no Models, Services, or Helpers folders**. The officer editor is a small form
class inside `Officerr Records.cs`. Shared face-image operations stay in `Registration.cs`.
Original filenames such as `Form1.cs` and `Officerr Records.cs` are intentionally not renamed.

## 2. Features and attendance rules

- Student CRUD with unique student numbers and optional unique email addresses.
- Exactly **five valid webcam face pictures** required for a new student. Student data
  and images save in one transaction; failed saves do not leave partial registrations.
- Edit student details without recapturing, or replace all five face images.
- Multi-day events generate Morning and/or Afternoon sessions for **every date** in the range.
- Separate login and optional logout for each student/session. Repeated scans do not create duplicates.
- Active events can require logout; a missing required logout is flagged **Missing**.
  A recorded login still counts as **Present**; missing logout does not add another absence.
- **Pending** means a roster student has not logged in and the session has not ended.
  **Absent** means the session ended without a login. Only absent sessions earn points.
- Each absent student/session adds **10 points**. Deductions/clearings leave absence counts intact.
- Admins create officer accounts. Officers use the same login screen and initially have
  ordinary operational access, but cannot manage officers, administrators, permissions or password resets.
- Admins may revoke officer access to operational modules in Rules and Permission.
  Dashboard remains available to every signed-in account. Permission checks also protect save operations.
- Rules are informational notices; editing their descriptions does **not** change the fixed ten-point policy.

### Event roster (important)

Saving an event includes currently **Active** students who have five saved faces and
match its department (or All Departments). Use **View roster** to inspect participants.
Editing an event **before its first session starts** refreshes this roster and regenerates its sessions.

Students registered or re-activated later are added automatically to every matching event
that still has a session ending in the future. A session that had already ended before the
student was registered is never counted as an absence.
For events that have not started yet, saving a student also follows their latest department and
status (for example, an Inactive student leaves the upcoming roster).

Once an event has started, only its name and venue can change. Its schedule, department and
logout setting are locked, and existing roster rows keep their department/year snapshot.
This protects attendance and obligation history.
For attendance, the student must still be Active and fully face-registered.

A two-day event with both sessions enabled produces four independent attendance periods:
Day 1 Morning, Day 1 Afternoon, Day 2 Morning, Day 2 Afternoon.

## 3. Requirements

- **64-bit Windows 10 or 11**.
- **Visual Studio 2022**, with the **.NET desktop development** workload.
- **.NET Framework 4.7.2 Developer Pack / targeting pack**.
- .NET SDK installed by Visual Studio (the project uses SDK-style project loading).
- **XAMPP 8.2.x** with **MariaDB 10.4.x**, or a compatible MySQL installation.
  The import and database workflows were tested against MariaDB **10.4.34**.
- **Microsoft Visual C++ Redistributable 2015-2022, x64** for native OpenCV.
- A USB or built-in webcam, working lighting, and Windows camera permission.
- At least **1366 x 768** display at **100% Windows scaling** for the original fixed-size forms.
- Internet for initial NuGet restore.

In Visual Studio Installer choose **Modify > .NET desktop development**.
Under Individual components ensure the .NET Framework 4.7.2 targeting pack is selected.
Do not retarget this project to .NET Core, x86, or another framework just to fix a missing targeting pack.

## 4. Database installation

**Back up an existing database first. Re-importing the included SQL deletes application data,
including accounts, faces, attendance, rosters and obligations. It is a fresh setup, not a migration
from the previous feature branch.**

1. Start XAMPP Control Panel.
2. Start **Apache** and **MySQL**.
3. Open `http://localhost/phpmyadmin`.
4. Choose **Import** at server level.
5. Select [`scc_records_management (1).sql`](./scc_records_management%20%281%29.sql) from this repository's root.
6. Click **Import / Go**.
7. Confirm the `scc_records_management` database appears without errors.

The script contains `CREATE DATABASE`, `USE`, tables, keys, constraints, indexes,
two reporting views, four starter departments, rules, officer permissions and a starter administrator.
Your importing account needs permission to create tables and views.

From a MySQL command prompt, the equivalent is:

```sql
SOURCE C:/your-folder/SCC_RecordsManagementSystem/scc_records_management (1).sql;
```

Default login:

```text
Username: admin
Password: admin123
```

Change it after setup using **Rules and Permission > Manage Accounts > Reset Password**.
Passwords are stored as BCrypt hashes, not plain text.

### Tables

| Table/view | Purpose |
|---|---|
| `accounts`, `officers` | Admin/officer credentials and officer personal details |
| `departments`, `students` | Department and student information |
| `face_images` | Five numbered PNG face crops for each registered student |
| `attendance_events`, `event_sessions` | Dates, department scope, times, logout setting |
| `event_students` | Saved event roster, including department/year at enrollment |
| `event_attendance` | One row per student/session, with login and optional logout |
| `obligation_adjustments` | Event-scoped point deductions, reasons, actor and timestamp |
| `login_history` | Successful logins and explicit logout times |
| `rules`, `role_permissions` | Informational rules and officer module permissions |
| `password_resets` | Preserved original table; not used by current admin-reset workflow |
| `attendance_records` | View deriving Present / Absent / Pending and logout status |
| `obligation_balances` | View calculating absences, points, deductions and balance |

Deleting a student cascades their images, roster entries, attendance and adjustments.
Deleting an event cascades its sessions, roster, attendance and event-specific adjustments.
Deleting an officer removes their login account. Audit rows retain the username or show a deleted account.
A department referenced by students, officers, events or saved rosters cannot be deleted.
Prefer marking a student **Inactive** when history must be retained rather than deleting them.

## 5. Configure the connection

Open [`Database.cs`](./SCCRecordsManagementSystem/Database.cs). Edit:

```csharp
private static string connectionString =
    "Server=localhost;Database=scc_records_management;Uid=root;Pwd=;";
```

This matches a local XAMPP installation with no root password.
Change `Server`, `Uid`, and `Pwd` as needed; add `Port=3307;` if your MySQL port is different.
For a classroom project the connection string remains in source as requested.
Use a restricted database account, not an unrestricted root account, for any real deployment.

Attendance boundaries and saved scan timestamps use **the database server's current local time**.
Set the database server and Windows PC to the campus time zone. The dashboard clock uses Windows time.
Verify database time with `SELECT NOW();` before an event.

## 6. NuGet packages (exact direct references)

The project targets **.NET Framework 4.7.2**, C# 7.3, Any CPU with Prefer 32-bit disabled.

| Package | Version | Purpose |
|---|---|---|
| BCrypt.Net-Next | **4.2.0** | Password hashing |
| MySql.Data | **6.9.12** | MySQL/MariaDB connector, matching the sample's API style |
| OpenCvSharp4 | **4.11.0.20250507** | Webcam, Haar detection, LBPH face recognition |
| OpenCvSharp4.runtime.win | **4.11.0.20250507** | Matching native Windows OpenCV runtime |

NuGet restores transitive dependencies automatically.
The original `packages.config` and old checked-in package/build files were preserved, but
**PackageReference entries in the `.csproj` are now authoritative**. Do not copy the instructor's
external MySql.Data DLL into the project. Its broken external HintPath is no longer used.

### Visual Studio installation steps

1. Open `SCCRecordsManagementSystem/SCCRecordsManagementSystem.sln`.
2. Go to **Tools**.
3. Select **NuGet Package Manager**.
4. Select **Manage NuGet Packages for Solution**.
5. Open **Browse**.
6. Search for a package from the table above.
7. Select its exact documented version.
8. Select the **SCCRecordsManagementSystem** project.
9. Click **Install**.
10. Accept the license prompt. Repeat for the other packages if missing.

Usually no manual installation is needed: right-click the solution and choose **Restore NuGet Packages**.
For Package Manager Console:

```powershell
Install-Package BCrypt.Net-Next -Version 4.2.0 -ProjectName SCCRecordsManagementSystem
Install-Package MySql.Data -Version 6.9.12 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4 -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
Install-Package OpenCvSharp4.runtime.win -Version 4.11.0.20250507 -ProjectName SCCRecordsManagementSystem
```

`System.Windows.Forms.DataVisualization` is a built-in .NET Framework reference for the original charts.
The included `haarcascade_frontalface_default.xml` is copied beside the executable.
Keep its original OpenCV license notice.

## 7. Build and run

1. Open the solution above in Visual Studio 2022 on Windows.
2. Restore NuGet packages and select **Debug / Any CPU**. Do not enable Prefer 32-bit.
3. Choose **Build > Rebuild Solution**.
4. Start MySQL and import/configure the database.
5. Press **F5**, or **Ctrl+F5** to run without debugging.
6. Log in using the starter account or an admin-created officer account.
7. Use the existing sidebar buttons to switch pages. Use **LogOut** to return to login.
8. Use the top-right X to close. Explicit LogOut records the logout time; closing/crashing
   the application leaves the history logout field empty (it is not a heartbeat).

On Windows the output is normally `SCCRecordsManagementSystem/bin/Debug/net472/`.
If moving the app, copy the **whole output directory**, including native runtime files and detector XML.
Do not run an old executable from the repository's previously checked-in build folders.

## 8. Register, view, edit and delete students

1. Create needed departments in **Departments** first.
2. Open **Student Records > Add Students**.
3. Enter unique student ID, first/last names, department, year and gender.
   Middle name, email and contact number are optional. Email/phone formats are validated.
4. The webcam starts automatically when the Add Student window opens (or click **Start Camera**).
   The app tries the built-in camera first, then USB cameras 1 and 2.
   Windows Settings > Privacy & security > Camera must allow camera access and access for
   desktop applications. Close Teams/Zoom/Camera if they hold the webcam.
5. The preview works like a mirror. A **green box** means the face is good; an **orange box**
   means the message under the preview tells you what to fix (move closer, move to the center,
   more light, less light, hold still, only one person). Small faces far in the background
   (posters, people walking behind) are ignored, so they do not cause "multiple faces".
6. Follow the live tip, then click **Capture**. Five poses are suggested in order: straight,
   slightly left, slightly right, slightly up, and normal/smile.
7. After each capture the message shows **Face N of 5 captured. Next: ...** for two seconds.
   No face, multiple faces, small/edge/dark/bright/blurry faces and repeated identical frames are
   rejected with "The picture was not saved."
8. **Retake All** asks for confirmation, clears the captured images and starts the camera again.
   **Reset** clears inputs and new captures.
9. **Save Registration** is rejected until all five pictures are present.
   Cancel closes without saving.

Select a row in Student Records to view its picture and details. The details panel also shows an
attendance summary: present and absent sessions, current obligation balance and last scan time.
Search by name/ID; filter by department, year or Active/Inactive.
Use **Edit** or double-click to update details. Checking **Re-register all 5 face pictures**
starts the camera; the first new capture clears the old thumbnails so old and new pictures are
never mixed. Unchecking it shows the saved pictures again, and saving keeps them.
If a saved picture is damaged, the editor still opens and requires five new captures to repair it.
Capture is blocked while the camera is stopped or its latest frame read has failed.
**Delete** asks for confirmation and removes related records; use Inactive instead to keep history.

## 9. Create events and record attendance

1. Register participants first (students registered later are still added automatically).
2. Open **Event Management > Add Events**.
3. Enter name, venue, start/end dates and department scope.
4. Enable Morning and/or Afternoon, set start/end times, and optionally require logout.
   Enabled sessions cannot overlap; each end must follow its start. Overnight sessions are not supported.
   The same schedule repeats each day; use separate events when different days need different times.
5. Save, then check **View roster**. Before the event starts, **Edit > Save** refreshes participants.
   The editor previews the number of sessions. After the event starts, protected schedule controls
   are disabled; only name and venue remain editable.
6. Open **Face Recognition** during a session. The first active session is selected automatically.
   If multiple sessions/events are open, select the correct one. The line under the session list
   shows how many students and face pictures were loaded (for example
   "3 student(s) with registered faces (15 face pictures loaded)").
7. Choose **Log In**. Stand alone in front of the camera. The box around the face shows what is
   happening: **gray** = no face, **orange** = fix the face (message shown), **gold** = recognizing
   (1/3, 2/3), **green** = recognized, **red** = unknown face.
   Attendance is recorded automatically after three consecutive matching frames.
8. The result card shows **Student Recognized**, the student's information, **Status: Present**
   and the saved time. If it was already recorded, it shows the original time instead.
   The card stays while the student is in front of the camera and clears about 3 seconds after
   they leave, so the same student is not processed again and again. The camera keeps running.
9. For departure, choose **Log Out** and scan again **before the session end time**.
10. **Another Person** clears the card. **Restart / Reload** reloads active sessions and trained faces.
    Use it after registration changes, a session transition or reconnecting the camera.

The database enforces one student/session record, and transactions serialize competing scans.
An existing login/logout returns its original time instead of creating duplicates.
Logging out before logging in, scanning a closed session, an inactive student or a student
outside the roster is refused with an explanation.
No manual presence button substitutes for face recognition.

The event list supports **All Events / Upcoming / In Progress / Completed** and name/venue search.
**In Progress** covers the whole event, including gaps between sessions; it does not mean a scan is
currently allowed. Select **Sessions** to see each session's opening/closing time, Open/Closed/Upcoming
status, expected students, Present/Absent/Pending counts and missing required logouts.
Double-click an event to edit it. The roster also shows each participant's current Active/Inactive state.

### How recognition works

Both screens use the same helper methods in `Registration.cs` (`Detect`, `CheckFace`, `PrepareFace`).

1. **Detect:** the Haar classifier finds faces of at least 60 x 60 pixels. Faces smaller than 60%
   of the biggest face are ignored (background faces), so only a real second person counts as
   "multiple faces".
2. **Check:** exactly one face, at least 90 pixels wide, not touching the edge of the picture,
   brightness between 60 and 200, and sharp enough (not blurry).
3. **Register:** five 160 x 160 color PNG crops are saved in the database.
4. **Prepare:** for training and matching, each face becomes grayscale, 100 x 100, with equalized contrast.
5. **Train:** LBPH trains on the saved pictures of Active students who have all five pictures,
   labeled by student ID. A damaged picture is skipped and reported instead of stopping attendance.
6. **Match:** lower distance is better; `MatchThreshold = 70` in `FaceRecognition.cs` is the cutoff.
   The same student must match in three frames in a row before the database is written.
   A face is only called **unknown** after five non-matching frames in a row, so one unclear frame
   does not show a wrong "unknown" message.

The preview is mirrored only on screen; the saved and compared pictures are not mirrored.

This is a classroom-level recognizer, **not biometric identity proof or liveness detection**.
Printed photos may fool it, similar faces may be confused, and the threshold must be evaluated
with the actual students, webcam and lighting. Do not blindly increase the threshold:
it can accept unknown people. No Python or external recognition service is used.
The app does not upload face images. Obtain consent and protect database backups.

## 10. Attendance, obligations and reports

All record grids have readable headings, alternating row colors, formatted dates, sortable headings,
and draggable column order. Scroll horizontally for additional details. Empty lists and database
errors are explained; failed refreshes clear stale records instead of leaving them looking current.

**Dashboard**

- Student, officer and event totals refresh every 15 seconds.
- Select an event session above the chart to inspect its Present/Absent/Pending totals.
  Selection is retained during refresh; initially an active session is preferred.
- Recent attendance includes the latest 20 logins across events. The clock uses Windows time.
- Login History is available only to admins.

**Departments**

- Search by code, name or department head; inspect student/officer counts.
- Add, edit (also by double-click) or delete with confirmation.
- Referenced departments cannot be deleted. Cancel closes the editor without saving.

**Attendance Monitoring**

- Select event, session, department, year and optional date; search student name/ID.
- Present, Absent and Pending are three independent checkboxes. All checked shows all statuses;
  none checked shows no records.
- Filter logout state separately, for example **Missing** to find required departures not scanned.
- **Today** enables today's date filter; **Clear filters** restores all events, sessions and statuses.
- **Refresh** updates time-dependent statuses; **Export CSV** saves the filtered, sorted visible grid.
- Rows include session start/end, time in/out, presence status and logout status.

**Obligations**

- Search and filter by event, department and year.
- All Events shows totals; select **one event** to adjust a selected student's balance.
- Choose **Deduct / Clear**, enter a positive deduction or select Clear balance to zero,
  and enter a required reason.
- Deductions cannot exceed the current balance. They cannot carry over to another event.
- The adjustment dialog displays the balance, limits the deduction, requires confirmation, and
  refuses zero-balance adjustments. The database checks the balance again when saving.
- **History** (or double-click a row) shows each adjustment with event, reason, time and account.
- **Export CSV** saves the visible obligation list for Excel.
- Clearing points never clears the absence count. Later missed sessions can add new points.
- Summary cards count distinct students. All-event rows may separate historical department/year
  snapshots when a student changed course or year; their points are still correctly totaled.

**Reports and Analysis**

- Choose event, date range, department, optional student name/ID and report type, then Generate.
  Selecting an event fills the From/To dates with that event's dates. Changing the report type,
  event or department regenerates automatically; press Enter in the search box to generate.
- Sessions: present/absent totals and attendance percentages by session and department.
- Most absent: students ranked by absent session count and points earned in the selected date range.
- Obligations: current event balances across **all dates**; this is explicitly labeled.
- Student attendance: one row per student/completed session, including status and login/logout times.
- Missing logout: completed-session rows where a required logout was not recorded.
- The chart uses the selected completed-session date range. Present/Absent checkboxes control its series.
- **Export CSV** saves the currently generated grid for Excel. Empty reports cannot be exported.
  Exports preserve visible column order and neutralize spreadsheet formula prefixes.

**Officer Records**

Only admins can add/edit/delete officers and set their username/password.
Details include officer number, name, department, year, gender, optional email/contact,
position and school year. Leave password blank when editing to keep it.
Search by officer number, name, username or position, and filter by department/year.
Double-click a row to edit it. Deletion also removes the officer's login.
Officers are separate from students: an officer who also needs student attendance must
be registered separately in Student Records with five face pictures.

**Rules and Permission / Login History**

Admins can add/edit/delete informational rules and change officer module access.
**Manage Accounts** lists every login with role, creation date and last login. From there admins can
create another administrator, reset the selected account's password, or delete another administrator.
You cannot delete the account you are signed in with; officer logins are managed in Officer Records.
There is no unauthenticated password reset.
Open Login History from Dashboard to search usernames and filter successful logins by date.
Refresh and CSV export are available. **No explicit logout** is not proof that someone is online:
closing or crashing the application also leaves the logout time empty.

## 11. Troubleshooting

| Problem | What to check |
|---|---|
| Database operation failed | Start MySQL; check credentials/port in Database.cs; import the correct SQL, including views. |
| ID, username, email or name already exists | Choose a unique identifier/email/department name. Do not reuse another student's ID. |
| Cannot delete department | It is referenced by students, officers or event history; keep it or remove dependent records intentionally. |
| Face detector missing | Rebuild and keep `haarcascade_frontalface_default.xml` beside the executable. |
| Camera library could not load | Restore both matching OpenCvSharp packages, use Windows x64, install Visual C++ 2015-2022 x64. |
| Webcam unavailable | Connect a camera (cameras 0, 1 and 2 are tried), allow Windows desktop camera access, close other camera apps. |
| Webcam disconnected | Reconnect and use Start Camera / Restart. |
| No face / multiple faces / poor quality | Follow the message under the preview: face the camera, remove other people from view, move closer, move to the center, improve or soften the lighting, hold still. |
| Box stays gold ("Recognizing your face...") | Look straight at the camera with similar lighting to registration; if it never turns green, re-register the student. |
| Unknown face | Check Active status and five pictures; re-register under similar lighting, then Restart / Reload. |
| Missing/incomplete saved faces | Students without exactly five pictures are not loaded. Damaged pictures are skipped and counted in red under the session list; edit the student and capture five new images. |
| No active event | Check database server time and session times; wait/start correct event, then Restart / Reload. |
| Student not on roster | Check the event's department scope and the student's Active status/five pictures, then save the student again. Completed events never add new students. |
| Cannot edit schedule | Event has already started. Its history is protected; create a replacement event if necessary. |
| Password forgotten | Ask admin to reset it. If all admin passwords are lost, restore a backup or intentionally re-import (erases data). |
| Form clipped | Use at least 1366 x 768 and 100% Windows display scaling. |
| Target framework missing | Install .NET Framework 4.7.2 Developer Pack in Visual Studio Installer. |
| Linux resource/chart/native errors | Run on Windows. The app uses Windows Forms, Windows chart controls and a Windows native camera package. |

## 12. Verification and Windows acceptance checklist

Verified in the development environment:

- All original form source compiles together: **0 compiler errors and warnings**.
  Linux compilation used a temporary resource-build compatibility package, not added to this project.
- Fresh and repeated SQL imports on MariaDB 10.4.34 (14 tables, 2 views, 16 foreign keys).
- **106 C# database workflow checks passed** (0 failed): five-image requirement,
  validation, duplicates, student CRUD/re-registration, multi-day sessions, automatic roster updates,
  absence cutoff for late registrations, login/logout duplicates, every attendance filter, absence points,
  deductions/clearing/history, report filters, officers, permissions, account deletion rules, cascades
  and CSV safety. These used a temporary C# harness and synthetic images, not live face recognition.
  They were run before the face detection update, which did not change the database or save code.
- **27 Mono form checks passed** (0 failed): original forms open, student attendance summary, saved face
  thumbnails, event status list, locked-schedule notice, attendance/obligation/login-history grids,
  Manage Accounts button and friendly missing-camera-library feedback.
  Dashboard and Reports and Analysis were skipped (2) because Mono does not implement Windows charts.
- After the face detection update:
  - **13 real OpenCV checks passed** (0 failed) using the exact `Detect`/`CheckFace`/`PrepareFace` code,
    OpenCvSharp 4.11.0.20250507 (Linux runtime) and public OpenCV sample photos: one face accepted;
    no face, two people, dark, blurry, edge and far-away faces rejected; a small background face ignored;
    five pictures registered for two people; both recognized (distance 45 and 34); 11 unregistered
    people from a group photo all stayed unknown.
  - **9 Mono form checks passed** (0 failed) for Add/Edit Student and Face Recognition: automatic camera
    start, damaged/valid saved pictures, re-register check/uncheck, no-session and camera-library messages.
  - SQL file re-imported on MariaDB 10.4; the new training query loads only Active students with five pictures.
- All original files remain; there are no deleted original forms or separated sidebar files.

**Not claimed as verified:** real webcam recognition accuracy, Windows chart rendering,
and end-to-end Visual Studio execution. Linux/Mono is not a substitute for these.
Before using at an actual event, test on the target Windows PC:

1. Import SQL into a test database and build in Visual Studio.
2. Log in as admin, create an officer, and confirm that officer cannot manage accounts.
3. Add a department; create two students with five real captures each.
4. Verify 0/4 pictures cannot save; verify no-face/multiple-face/poor-light capture rejection.
5. Edit/search/view a student, re-register faces and confirm five images remain.
6. Create a two-day morning/afternoon event and inspect all four sessions/roster.
7. Recognize each registered student, then an unregistered person. Confirm no unknown attendance.
8. Repeat a scan and verify only one row; log out and repeat logout.
9. Disconnect/reconnect webcam and temporarily stop MySQL; verify clear errors and successful retry.
10. After a session ends, check absent count, 10-point penalties, partial deduction and clear-to-zero.
11. Check all attendance/report filters and export a CSV.
12. Check dashboard session selection/chart, both new detailed report types, permissions, password reset and login history.
13. Delete disposable test students/events and verify dependent records are removed.