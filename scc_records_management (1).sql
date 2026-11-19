-- SCC setup for MariaDB 10.4 / MySQL 8.
-- WARNING: importing again erases this application's records. Back up first.
CREATE DATABASE IF NOT EXISTS scc_records_management CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE scc_records_management;
DROP VIEW IF EXISTS attendance_records;
DROP VIEW IF EXISTS obligation_balances;
SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS obligation_adjustments, event_attendance, event_students, event_sessions,
    attendance_events, face_images, officers, students, departments, login_history,
    rules, role_permissions, password_resets, accounts;
SET FOREIGN_KEY_CHECKS=1;
-- Original account and password-reset tables are retained below.
-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 17, 2026 at 04:06 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
-- Session times use the database server's local time; configure it to campus time.


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `scc_records_management`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `account_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `accounts`
--

INSERT INTO `accounts` (`account_id`, `username`, `password`, `created_at`) VALUES
(1, 'admin', '$2a$11$tfSfkyeCI1XhIswUUl57S.sf9roXRb2z/7W82hSVjmocduKW3I1uC', '2026-11-17 14:33:40');

-- --------------------------------------------------------

--
-- Table structure for table `password_resets`
--

CREATE TABLE `password_resets` (
  `id` int(11) NOT NULL,
  `account_id` int(11) NOT NULL,
  `token_hash` varchar(255) NOT NULL,
  `expires_at` datetime NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`account_id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `password_resets`
--
ALTER TABLE `password_resets`
  ADD PRIMARY KEY (`id`),
  ADD KEY `account_id` (`account_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `password_resets`
--
ALTER TABLE `password_resets`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `password_resets`
--
ALTER TABLE `password_resets`
  ADD CONSTRAINT `password_resets_ibfk_1` FOREIGN KEY (`account_id`) REFERENCES `accounts` (`account_id`) ON DELETE CASCADE;
COMMIT;

ALTER TABLE accounts ADD role VARCHAR(20) NOT NULL DEFAULT 'Admin',
    ADD CONSTRAINT chk_role CHECK (role IN ('Admin','Officer'));
-- Starter login: admin / admin123. Change it in Rules and Permission.
UPDATE accounts SET password='$2a$11$ImieFcUoeFA4gmZBkIB1m.TAvs7tQi5eExMCNZ0s4ZV0CcaFKxmfa' WHERE username='admin';

CREATE TABLE departments (
 department_id INT AUTO_INCREMENT PRIMARY KEY,
 department_code VARCHAR(20) NOT NULL UNIQUE,
 department_name VARCHAR(100) NOT NULL UNIQUE,
 department_head VARCHAR(100), contact_email VARCHAR(100),
 description VARCHAR(255), academic_level VARCHAR(30),
 created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;
INSERT INTO departments (department_code,department_name,academic_level) VALUES
 ('BSIT','Bachelor of Science in Information Technology','Undergraduate'),
 ('BSBA','Bachelor of Science in Business Administration','Undergraduate'),
 ('BEED','Bachelor of Elementary Education','Undergraduate'),
 ('BSED','Bachelor of Secondary Education','Undergraduate');

CREATE TABLE students (
 student_id INT AUTO_INCREMENT PRIMARY KEY,
 student_number VARCHAR(20) NOT NULL UNIQUE,
 first_name VARCHAR(50) NOT NULL, middle_name VARCHAR(50), last_name VARCHAR(50) NOT NULL,
 department_id INT NOT NULL, year_level VARCHAR(20) NOT NULL, gender VARCHAR(20) NOT NULL,
 email VARCHAR(100) UNIQUE, contact_number VARCHAR(20),
 status VARCHAR(10) NOT NULL DEFAULT 'Active',
 face_registered TINYINT NOT NULL DEFAULT 0,
 created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 FOREIGN KEY (department_id) REFERENCES departments(department_id) ON DELETE RESTRICT,
 CHECK (status IN ('Active','Inactive')), CHECK (face_registered IN (0,1)),
 INDEX idx_student_name(last_name,first_name)
) ENGINE=InnoDB;
CREATE TABLE face_images (
 face_image_id INT AUTO_INCREMENT PRIMARY KEY, student_id INT NOT NULL,
 image_number TINYINT NOT NULL, image_data MEDIUMBLOB NOT NULL,
 UNIQUE(student_id,image_number), CHECK(image_number BETWEEN 1 AND 5),
 FOREIGN KEY(student_id) REFERENCES students(student_id) ON DELETE CASCADE
) ENGINE=InnoDB;
CREATE TABLE officers (
 officer_id INT AUTO_INCREMENT PRIMARY KEY, account_id INT NOT NULL UNIQUE,
 officer_number VARCHAR(20) NOT NULL UNIQUE,
 first_name VARCHAR(50) NOT NULL, middle_name VARCHAR(50), last_name VARCHAR(50) NOT NULL,
 department_id INT NOT NULL, year_level VARCHAR(20) NOT NULL, gender VARCHAR(20) NOT NULL,
 email VARCHAR(100) UNIQUE, contact_number VARCHAR(20),
 position VARCHAR(60) NOT NULL, school_year VARCHAR(20) NOT NULL,
 created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
 FOREIGN KEY(account_id) REFERENCES accounts(account_id) ON DELETE CASCADE,
 FOREIGN KEY(department_id) REFERENCES departments(department_id) ON DELETE RESTRICT
) ENGINE=InnoDB;
CREATE TABLE attendance_events (
 event_id INT AUTO_INCREMENT PRIMARY KEY, event_name VARCHAR(120) NOT NULL,
 venue VARCHAR(120), start_date DATE NOT NULL, end_date DATE NOT NULL,
 department_id INT, requires_logout TINYINT NOT NULL DEFAULT 0,
 morning_enabled TINYINT NOT NULL, morning_start TIME NOT NULL, morning_end TIME NOT NULL,
 afternoon_enabled TINYINT NOT NULL, afternoon_start TIME NOT NULL, afternoon_end TIME NOT NULL,
 created_by INT, created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
 FOREIGN KEY(department_id) REFERENCES departments(department_id) ON DELETE RESTRICT,
 FOREIGN KEY(created_by) REFERENCES accounts(account_id) ON DELETE SET NULL,
 CHECK(end_date>=start_date), CHECK(morning_enabled=1 OR afternoon_enabled=1),
 CHECK(morning_enabled=0 OR morning_end>morning_start),
 CHECK(afternoon_enabled=0 OR afternoon_end>afternoon_start)
) ENGINE=InnoDB;
CREATE TABLE event_sessions (
 session_id INT AUTO_INCREMENT PRIMARY KEY, event_id INT NOT NULL,
 session_name VARCHAR(20) NOT NULL, starts_at DATETIME NOT NULL, ends_at DATETIME NOT NULL,
 UNIQUE(event_id,starts_at,session_name), CHECK(ends_at>starts_at),
 FOREIGN KEY(event_id) REFERENCES attendance_events(event_id) ON DELETE CASCADE,
 INDEX idx_session_time(starts_at,ends_at)
) ENGINE=InnoDB;
-- A saved event roster keeps old absences stable if a student later changes course/status.
CREATE TABLE event_students (
 event_id INT NOT NULL, student_id INT NOT NULL,
 department_id INT NOT NULL, year_level VARCHAR(20) NOT NULL,
 PRIMARY KEY(event_id,student_id),
 FOREIGN KEY(event_id) REFERENCES attendance_events(event_id) ON DELETE CASCADE,
 FOREIGN KEY(student_id) REFERENCES students(student_id) ON DELETE CASCADE,
 FOREIGN KEY(department_id) REFERENCES departments(department_id) ON DELETE RESTRICT
) ENGINE=InnoDB;
CREATE TABLE event_attendance (
 session_id INT NOT NULL, student_id INT NOT NULL, time_in DATETIME NOT NULL,
 time_out DATETIME, PRIMARY KEY(session_id,student_id),
 FOREIGN KEY(session_id) REFERENCES event_sessions(session_id) ON DELETE CASCADE,
 FOREIGN KEY(student_id) REFERENCES students(student_id) ON DELETE CASCADE,
 CHECK(time_out IS NULL OR time_out>=time_in)
) ENGINE=InnoDB;
CREATE TABLE obligation_adjustments (
 adjustment_id INT AUTO_INCREMENT PRIMARY KEY, event_id INT NOT NULL, student_id INT NOT NULL,
 points_deducted INT NOT NULL, reason VARCHAR(255) NOT NULL, account_id INT,
 created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
 FOREIGN KEY(event_id,student_id) REFERENCES event_students(event_id,student_id) ON DELETE CASCADE,
 FOREIGN KEY(account_id) REFERENCES accounts(account_id) ON DELETE SET NULL,
 CHECK(points_deducted>0)
) ENGINE=InnoDB;
CREATE TABLE login_history (
 login_id INT AUTO_INCREMENT PRIMARY KEY, account_id INT,
 username VARCHAR(50) NOT NULL, logged_in DATETIME NOT NULL, logged_out DATETIME,
 FOREIGN KEY(account_id) REFERENCES accounts(account_id) ON DELETE SET NULL,
 INDEX idx_login_time(logged_in)
) ENGINE=InnoDB;
CREATE TABLE rules (
 rule_id INT AUTO_INCREMENT PRIMARY KEY, description VARCHAR(255) NOT NULL,
 active TINYINT NOT NULL DEFAULT 1
) ENGINE=InnoDB;
INSERT INTO rules(description) VALUES ('Each missed event session adds 10 obligation points.'),
 ('Students must register five face pictures before joining an event.'),
 ('Scan Log Out before the session ends when the event requires it.');
CREATE TABLE role_permissions (
 module VARCHAR(40) PRIMARY KEY, allowed TINYINT NOT NULL DEFAULT 1
) ENGINE=InnoDB;
INSERT INTO role_permissions(module) VALUES ('Student Records'),('Departments'),
 ('Event Management'),('Face Recognition'),('Attendance Monitoring'),('Obligations'),('Reports and Analysis');

CREATE VIEW attendance_records AS
 SELECT e.event_id, es.session_id, es.starts_at, es.ends_at, e.event_name, es.session_name,
 s.student_id, s.student_number, CONCAT_WS(' ',s.first_name,s.middle_name,s.last_name) AS full_name,
 r.department_id, d.department_code, r.year_level,
 a.time_in,a.time_out,
 CASE WHEN a.time_in IS NOT NULL THEN 'Present' WHEN es.ends_at<=NOW() THEN 'Absent' ELSE 'Pending' END AS status,
 CASE WHEN a.time_in IS NULL THEN '-' WHEN a.time_out IS NOT NULL THEN 'Complete'
      WHEN e.requires_logout=0 THEN 'Optional' WHEN es.ends_at<=NOW() THEN 'Missing' ELSE 'Pending' END AS logout_status
 FROM event_sessions es JOIN attendance_events e ON e.event_id=es.event_id
 JOIN event_students r ON r.event_id=e.event_id JOIN students s ON s.student_id=r.student_id
 JOIN departments d ON d.department_id=r.department_id
 LEFT JOIN event_attendance a ON a.session_id=es.session_id AND a.student_id=s.student_id
 -- Sessions that ended before the student was registered are skipped (never counted absent).
 WHERE s.created_at<es.ends_at OR a.time_in IS NOT NULL;
CREATE VIEW obligation_balances AS
 SELECT r.event_id,r.student_id,s.student_number,CONCAT_WS(' ',s.first_name,s.middle_name,s.last_name) AS full_name,
 r.department_id,d.department_code,r.year_level,
 COALESCE(a.absent_count,0) AS absent_count,COALESCE(a.absent_count,0)*10 AS absence_points,
 COALESCE(x.deducted,0) AS deducted_points,
 GREATEST(COALESCE(a.absent_count,0)*10-COALESCE(x.deducted,0),0) AS balance
 FROM event_students r JOIN students s ON s.student_id=r.student_id
 JOIN departments d ON d.department_id=r.department_id
 LEFT JOIN (SELECT event_id,student_id,COUNT(*) AS absent_count FROM attendance_records
            WHERE status='Absent' GROUP BY event_id,student_id) a ON a.event_id=r.event_id AND a.student_id=r.student_id
 LEFT JOIN (SELECT event_id,student_id,SUM(points_deducted) AS deducted FROM obligation_adjustments
            GROUP BY event_id,student_id) x ON x.event_id=r.event_id AND x.student_id=r.student_id;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
