-- =====================================================================
-- SCC Records Management System - database script
-- For XAMPP (MariaDB 10.4+) or MySQL 8.
--
-- HOW TO IMPORT: open phpMyAdmin > Import > choose this file > Import.
-- The script creates the database "scc_management" by itself.
--
-- WARNING: importing this file again DELETES all records of this system.
--          Export a backup first if you already have data.
--
-- Starter login:  username = admin   password = admin123
-- =====================================================================

CREATE DATABASE IF NOT EXISTS scc_management CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE scc_management;

SET NAMES utf8mb4;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";

-- Remove old objects so the file can be imported again without errors.
DROP VIEW IF EXISTS obligation_summary;
DROP VIEW IF EXISTS attendance_records;
SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS obligation_adjustments, event_attendance, event_students, event_sessions, events,
    face_images, officers, students, departments, login_history, rules, role_permissions,
    password_resets, accounts;
-- Tables used by older versions of this project.
DROP TABLE IF EXISTS attendance_events;
SET FOREIGN_KEY_CHECKS = 1;

-- ---------------------------------------------------------------------
-- 1. Accounts (people who can log in: the Admin and the Officers)
-- ---------------------------------------------------------------------
CREATE TABLE accounts (
  account_id INT(11) NOT NULL AUTO_INCREMENT,
  username   VARCHAR(50) NOT NULL,
  password   VARCHAR(255) NOT NULL,              -- BCrypt hash, never the plain password
  role       VARCHAR(20) NOT NULL DEFAULT 'Admin',
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (account_id),
  UNIQUE KEY uq_accounts_username (username),
  CONSTRAINT chk_accounts_role CHECK (role IN ('Admin', 'Officer'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Password of the starter admin account is: admin123
INSERT INTO accounts (account_id, username, password, role) VALUES
(1, 'admin', '$2a$11$ImieFcUoeFA4gmZBkIB1m.TAvs7tQi5eExMCNZ0s4ZV0CcaFKxmfa', 'Admin');

-- Kept from the original database (reserved for future password reset tokens).
CREATE TABLE password_resets (
  id         INT(11) NOT NULL AUTO_INCREMENT,
  account_id INT(11) NOT NULL,
  token_hash VARCHAR(255) NOT NULL,
  expires_at DATETIME NOT NULL,
  created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  KEY idx_password_resets_account (account_id),
  CONSTRAINT fk_password_resets_account FOREIGN KEY (account_id) REFERENCES accounts (account_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 2. Departments
-- ---------------------------------------------------------------------
CREATE TABLE departments (
  department_id   INT(11) NOT NULL AUTO_INCREMENT,
  department_name VARCHAR(100) NOT NULL,
  department_code VARCHAR(20) NOT NULL,
  department_head VARCHAR(100) NOT NULL,
  contact_email   VARCHAR(100) DEFAULT NULL,
  description     VARCHAR(255) DEFAULT NULL,
  academic_level  VARCHAR(30) NOT NULL,
  created_at      TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at      TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (department_id),
  UNIQUE KEY uq_departments_code (department_code),
  UNIQUE KEY uq_departments_name (department_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO departments (department_name, department_code, department_head, academic_level) VALUES
('Bachelor of Science in Information Technology', 'BSIT', 'To be assigned', 'Undergraduate'),
('Bachelor of Science in Business Administration', 'BSBA', 'To be assigned', 'Undergraduate'),
('Bachelor of Elementary Education', 'BEED', 'To be assigned', 'Undergraduate'),
('Bachelor of Secondary Education', 'BSED', 'To be assigned', 'Undergraduate');

-- ---------------------------------------------------------------------
-- 3. Students and their 5 face pictures
-- ---------------------------------------------------------------------
CREATE TABLE students (
  student_id      INT(11) NOT NULL AUTO_INCREMENT,
  student_number  VARCHAR(20) NOT NULL,
  first_name      VARCHAR(50) NOT NULL,
  middle_name     VARCHAR(50) DEFAULT NULL,
  last_name       VARCHAR(50) NOT NULL,
  department_id   INT(11) NOT NULL,
  year_level      VARCHAR(20) NOT NULL,
  gender          VARCHAR(20) NOT NULL,
  email           VARCHAR(100) DEFAULT NULL,
  contact_number  VARCHAR(20) DEFAULT NULL,
  status          VARCHAR(10) NOT NULL DEFAULT 'Active',
  face_registered TINYINT(1) NOT NULL DEFAULT 0,     -- 1 = all 5 face pictures are saved
  created_at      TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at      TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (student_id),
  UNIQUE KEY uq_students_number (student_number),
  UNIQUE KEY uq_students_email (email),
  KEY idx_students_name (last_name, first_name),
  KEY idx_students_department (department_id),
  CONSTRAINT fk_students_department FOREIGN KEY (department_id) REFERENCES departments (department_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT chk_students_status CHECK (status IN ('Active', 'Inactive')),
  CONSTRAINT chk_students_face CHECK (face_registered IN (0, 1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Deleting a student automatically deletes his/her face pictures (ON DELETE CASCADE).
CREATE TABLE face_images (
  face_image_id INT(11) NOT NULL AUTO_INCREMENT,
  student_id    INT(11) NOT NULL,
  image_number  TINYINT(4) NOT NULL,                 -- 1 to 5
  image_data    MEDIUMBLOB NOT NULL,                 -- 112 x 112 PNG picture of the face (lined up by the eyes)
  created_at    TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (face_image_id),
  UNIQUE KEY uq_face_images_student_number (student_id, image_number),
  CONSTRAINT fk_face_images_student FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE,
  CONSTRAINT chk_face_images_number CHECK (image_number BETWEEN 1 AND 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 4. Officers (each officer owns one login account with role 'Officer')
-- ---------------------------------------------------------------------
CREATE TABLE officers (
  officer_id     INT(11) NOT NULL AUTO_INCREMENT,
  account_id     INT(11) NOT NULL,
  officer_number VARCHAR(20) NOT NULL,
  first_name     VARCHAR(50) NOT NULL,
  middle_name    VARCHAR(50) DEFAULT NULL,
  last_name      VARCHAR(50) NOT NULL,
  department_id  INT(11) NOT NULL,
  year_level     VARCHAR(20) NOT NULL,
  gender         VARCHAR(20) NOT NULL,
  email          VARCHAR(100) DEFAULT NULL,
  contact_number VARCHAR(20) DEFAULT NULL,
  position       VARCHAR(60) NOT NULL,
  school_year    VARCHAR(20) NOT NULL,
  created_at     TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at     TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (officer_id),
  UNIQUE KEY uq_officers_account (account_id),
  UNIQUE KEY uq_officers_number (officer_number),
  UNIQUE KEY uq_officers_email (email),
  CONSTRAINT fk_officers_account FOREIGN KEY (account_id) REFERENCES accounts (account_id) ON DELETE CASCADE,
  CONSTRAINT fk_officers_department FOREIGN KEY (department_id) REFERENCES departments (department_id) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 5. Events, sessions, participants and attendance
--    One event can last many days. Every day can have a Morning,
--    Afternoon and/or Evening session. Each session is a separate attendance.
-- ---------------------------------------------------------------------
CREATE TABLE events (
  event_id          INT(11) NOT NULL AUTO_INCREMENT,
  event_name        VARCHAR(120) NOT NULL,
  venue             VARCHAR(120) NOT NULL,
  organizer         VARCHAR(120) DEFAULT NULL,
  start_date        DATE NOT NULL,
  end_date          DATE NOT NULL,
  department_id     INT(11) DEFAULT NULL,             -- NULL = all departments
  year_level        VARCHAR(20) DEFAULT NULL,         -- NULL = all year levels
  requires_logout   TINYINT(1) NOT NULL DEFAULT 0,    -- 1 = students must also scan Log Out
  morning_enabled   TINYINT(1) NOT NULL DEFAULT 0,
  morning_start     TIME NOT NULL DEFAULT '08:00:00',
  morning_end       TIME NOT NULL DEFAULT '12:00:00',
  afternoon_enabled TINYINT(1) NOT NULL DEFAULT 0,
  afternoon_start   TIME NOT NULL DEFAULT '13:00:00',
  afternoon_end     TIME NOT NULL DEFAULT '17:00:00',
  evening_enabled   TINYINT(1) NOT NULL DEFAULT 0,
  evening_start     TIME NOT NULL DEFAULT '18:00:00',
  evening_end       TIME NOT NULL DEFAULT '21:00:00',
  created_by        INT(11) DEFAULT NULL,
  created_at        TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at        TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (event_id),
  KEY idx_events_dates (start_date, end_date),
  CONSTRAINT fk_events_department FOREIGN KEY (department_id) REFERENCES departments (department_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_events_account FOREIGN KEY (created_by) REFERENCES accounts (account_id) ON DELETE SET NULL,
  CONSTRAINT chk_events_dates CHECK (end_date >= start_date),
  CONSTRAINT chk_events_one_session CHECK (morning_enabled = 1 OR afternoon_enabled = 1 OR evening_enabled = 1),
  CONSTRAINT chk_events_morning CHECK (morning_enabled = 0 OR morning_end > morning_start),
  CONSTRAINT chk_events_afternoon CHECK (afternoon_enabled = 0 OR afternoon_end > afternoon_start),
  CONSTRAINT chk_events_evening CHECK (evening_enabled = 0 OR evening_end > evening_start)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- One row = one attendance period (example: Day 1 Morning).
CREATE TABLE event_sessions (
  session_id   INT(11) NOT NULL AUTO_INCREMENT,
  event_id     INT(11) NOT NULL,
  session_date DATE NOT NULL,
  session_name VARCHAR(20) NOT NULL,                  -- Morning, Afternoon or Evening
  starts_at    DATETIME NOT NULL,
  ends_at      DATETIME NOT NULL,
  PRIMARY KEY (session_id),
  UNIQUE KEY uq_event_sessions (event_id, session_date, session_name),
  KEY idx_event_sessions_time (starts_at, ends_at),
  CONSTRAINT fk_event_sessions_event FOREIGN KEY (event_id) REFERENCES events (event_id) ON DELETE CASCADE,
  CONSTRAINT chk_event_sessions_time CHECK (ends_at > starts_at),
  CONSTRAINT chk_event_sessions_name CHECK (session_name IN ('Morning', 'Afternoon', 'Evening'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- The students who are expected to attend an event (the "roster").
-- added_at: sessions that ended before the student was added are never counted as absent.
CREATE TABLE event_students (
  event_id   INT(11) NOT NULL,
  student_id INT(11) NOT NULL,
  added_at   DATETIME NOT NULL,
  PRIMARY KEY (event_id, student_id),
  KEY idx_event_students_student (student_id),
  CONSTRAINT fk_event_students_event FOREIGN KEY (event_id) REFERENCES events (event_id) ON DELETE CASCADE,
  CONSTRAINT fk_event_students_student FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- One row per student per session. The PRIMARY KEY makes duplicate attendance impossible.
CREATE TABLE event_attendance (
  session_id  INT(11) NOT NULL,
  student_id  INT(11) NOT NULL,
  time_in     DATETIME NOT NULL,
  time_out    DATETIME DEFAULT NULL,
  recorded_by VARCHAR(20) NOT NULL DEFAULT 'Face Recognition',   -- or 'Manual'
  PRIMARY KEY (session_id, student_id),
  KEY idx_event_attendance_student (student_id),
  CONSTRAINT fk_event_attendance_session FOREIGN KEY (session_id) REFERENCES event_sessions (session_id) ON DELETE CASCADE,
  CONSTRAINT fk_event_attendance_student FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE,
  CONSTRAINT chk_event_attendance_time CHECK (time_out IS NULL OR time_out >= time_in)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 6. Obligations
--    Every absence = 10 points. Officers can add points (positive number)
--    or deduct / clear points (negative number). Each change is saved here.
-- ---------------------------------------------------------------------
CREATE TABLE obligation_adjustments (
  adjustment_id INT(11) NOT NULL AUTO_INCREMENT,
  student_id    INT(11) NOT NULL,
  points        INT(11) NOT NULL,
  reason        VARCHAR(255) NOT NULL,
  account_id    INT(11) DEFAULT NULL,
  created_at    TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (adjustment_id),
  KEY idx_obligation_adjustments_student (student_id),
  CONSTRAINT fk_obligation_adjustments_student FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE,
  CONSTRAINT fk_obligation_adjustments_account FOREIGN KEY (account_id) REFERENCES accounts (account_id) ON DELETE SET NULL,
  CONSTRAINT chk_obligation_adjustments_points CHECK (points <> 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- 7. Login history, rules and permissions
-- ---------------------------------------------------------------------
CREATE TABLE login_history (
  login_id   INT(11) NOT NULL AUTO_INCREMENT,
  account_id INT(11) DEFAULT NULL,
  username   VARCHAR(50) NOT NULL,
  role       VARCHAR(20) NOT NULL,
  logged_in  DATETIME NOT NULL,
  logged_out DATETIME DEFAULT NULL,
  PRIMARY KEY (login_id),
  KEY idx_login_history_time (logged_in),
  CONSTRAINT fk_login_history_account FOREIGN KEY (account_id) REFERENCES accounts (account_id) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE rules (
  rule_id     INT(11) NOT NULL AUTO_INCREMENT,
  description VARCHAR(255) NOT NULL,
  active      TINYINT(1) NOT NULL DEFAULT 1,
  created_at  TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at  TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (rule_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO rules (description) VALUES
('Every absence in an event session adds 10 obligation points.'),
('Students must register 5 face pictures before they can use face recognition attendance.'),
('If an event requires log out, students must scan Log Out before the session ends.'),
('Only one person may stand in front of the camera while attendance is being recorded.');

-- Which menus each role may open. The Admin can always open everything.
CREATE TABLE role_permissions (
  role    VARCHAR(20) NOT NULL,
  module  VARCHAR(40) NOT NULL,
  allowed TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (role, module),
  CONSTRAINT chk_role_permissions_role CHECK (role IN ('Admin', 'Officer'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO role_permissions (role, module, allowed) VALUES
('Admin', 'Departments', 1), ('Admin', 'Student Records', 1), ('Admin', 'Officer Records', 1),
('Admin', 'Face Recognition', 1), ('Admin', 'Event Management', 1), ('Admin', 'Attendance Monitoring', 1),
('Admin', 'Reports and Analysis', 1), ('Admin', 'Obligations', 1), ('Admin', 'Rules and Permission', 1),
('Officer', 'Departments', 1), ('Officer', 'Student Records', 1), ('Officer', 'Officer Records', 1),
('Officer', 'Face Recognition', 1), ('Officer', 'Event Management', 1), ('Officer', 'Attendance Monitoring', 1),
('Officer', 'Reports and Analysis', 1), ('Officer', 'Obligations', 1), ('Officer', 'Rules and Permission', 0);

-- ---------------------------------------------------------------------
-- 8. Views (saved SELECT queries used by the program)
-- ---------------------------------------------------------------------

-- One row per student per session with the computed attendance status:
--   Present = scanned in, Absent = session ended without a scan, Pending = session not finished yet.
CREATE VIEW attendance_records AS
SELECT e.event_id, e.event_name, es.session_id, es.session_date, es.session_name, es.starts_at, es.ends_at,
       s.student_id, s.student_number, CONCAT(s.last_name, ', ', s.first_name) AS full_name,
       s.department_id, d.department_code, s.year_level,
       a.time_in, a.time_out, a.recorded_by,
       CASE WHEN a.time_in IS NOT NULL THEN 'Present'
            WHEN es.ends_at <= NOW() THEN 'Absent'
            ELSE 'Pending' END AS status,
       CASE WHEN a.time_in IS NULL THEN '-'
            WHEN a.time_out IS NOT NULL THEN 'Logged Out'
            WHEN e.requires_logout = 0 THEN 'Not Required'
            WHEN es.ends_at <= NOW() THEN 'Missing Log Out'
            ELSE 'Waiting' END AS logout_status
FROM event_sessions es
JOIN events e ON e.event_id = es.event_id
JOIN event_students r ON r.event_id = e.event_id
JOIN students s ON s.student_id = r.student_id
JOIN departments d ON d.department_id = s.department_id
LEFT JOIN event_attendance a ON a.session_id = es.session_id AND a.student_id = s.student_id
WHERE es.ends_at > r.added_at OR a.time_in IS NOT NULL;

-- One row per student: absences x 10 points + adjustments = balance (never below zero).
CREATE VIEW obligation_summary AS
SELECT s.student_id, s.student_number, CONCAT(s.last_name, ', ', s.first_name) AS full_name,
       s.department_id, d.department_code, s.year_level, s.status,
       COALESCE(ab.absent_count, 0) AS absent_count,
       COALESCE(ab.absent_count, 0) * 10 AS absence_points,
       COALESCE(adj.adjustment_points, 0) AS adjustment_points,
       GREATEST(COALESCE(ab.absent_count, 0) * 10 + COALESCE(adj.adjustment_points, 0), 0) AS balance
FROM students s
JOIN departments d ON d.department_id = s.department_id
LEFT JOIN (SELECT student_id, COUNT(*) AS absent_count FROM attendance_records
           WHERE status = 'Absent' GROUP BY student_id) ab ON ab.student_id = s.student_id
LEFT JOIN (SELECT student_id, SUM(points) AS adjustment_points FROM obligation_adjustments
           GROUP BY student_id) adj ON adj.student_id = s.student_id;
