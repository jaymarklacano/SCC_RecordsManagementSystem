-- =====================================================================
-- SCC Records Management System - database file
-- Works with XAMPP (MariaDB 10.4+) and MySQL 5.7+.
--
-- HOW TO IMPORT (phpMyAdmin):
--   1. Start Apache and MySQL in the XAMPP Control Panel.
--   2. Open http://localhost/phpmyadmin
--   3. Click "Import" (top menu), choose this file, then click "Import".
--   The database "scc_records_management" is created automatically.
--
-- WARNING: importing this file again DELETES the old tables and data first.
--
-- Default login after import:  username = admin   password = admin123
-- =====================================================================

CREATE DATABASE IF NOT EXISTS `scc_records_management`
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `scc_records_management`;

-- Drop the tables first (children before parents) so the file can be imported again.
DROP TABLE IF EXISTS `attendance`;
DROP TABLE IF EXISTS `face_images`;
DROP TABLE IF EXISTS `students`;
DROP TABLE IF EXISTS `departments`;
DROP TABLE IF EXISTS `password_resets`;
DROP TABLE IF EXISTS `accounts`;

-- ---------------------------------------------------------------------
-- accounts: people who can log in to the system
-- ---------------------------------------------------------------------
CREATE TABLE `accounts` (
  `account_id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(50) NOT NULL,
  `password` VARCHAR(255) NOT NULL,            -- BCrypt hash, never the plain password
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`account_id`),
  UNIQUE KEY `uq_accounts_username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- password_resets: kept from the original database (the app resets the
-- password directly from the Forgot Password screen, so it is not used).
-- ---------------------------------------------------------------------
CREATE TABLE `password_resets` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `account_id` INT NOT NULL,
  `token_hash` VARCHAR(255) NOT NULL,
  `expires_at` DATETIME NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `idx_password_resets_account` (`account_id`),
  CONSTRAINT `fk_password_resets_account` FOREIGN KEY (`account_id`)
    REFERENCES `accounts` (`account_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- departments: courses/departments the students belong to
-- ---------------------------------------------------------------------
CREATE TABLE `departments` (
  `department_id` INT NOT NULL AUTO_INCREMENT,
  `department_code` VARCHAR(20) NOT NULL,
  `department_name` VARCHAR(100) NOT NULL,
  `department_head` VARCHAR(100) DEFAULT NULL,
  `contact_email` VARCHAR(100) DEFAULT NULL,
  `description` VARCHAR(255) DEFAULT NULL,
  `academic_level` VARCHAR(30) DEFAULT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`department_id`),
  UNIQUE KEY `uq_departments_code` (`department_code`),
  UNIQUE KEY `uq_departments_name` (`department_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- students: one row per student
-- face_registered = 1 only after all 5 face pictures were saved
-- ---------------------------------------------------------------------
CREATE TABLE `students` (
  `student_id` INT NOT NULL AUTO_INCREMENT,
  `student_number` VARCHAR(20) NOT NULL,        -- the school ID number, e.g. 2026-001
  `first_name` VARCHAR(50) NOT NULL,
  `middle_name` VARCHAR(50) DEFAULT NULL,
  `last_name` VARCHAR(50) NOT NULL,
  `department_id` INT NOT NULL,
  `year_level` VARCHAR(20) NOT NULL,
  `gender` VARCHAR(10) NOT NULL,
  `email` VARCHAR(100) DEFAULT NULL,
  `contact_number` VARCHAR(20) DEFAULT NULL,
  `status` VARCHAR(10) NOT NULL DEFAULT 'Active',
  `face_registered` TINYINT(1) NOT NULL DEFAULT 0,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`student_id`),
  UNIQUE KEY `uq_students_number` (`student_number`),
  UNIQUE KEY `uq_students_email` (`email`),     -- NULL emails are allowed many times
  KEY `idx_students_name` (`last_name`, `first_name`),
  KEY `idx_students_department` (`department_id`),
  CONSTRAINT `fk_students_department` FOREIGN KEY (`department_id`)
    REFERENCES `departments` (`department_id`) ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT `chk_students_status` CHECK (`status` IN ('Active', 'Inactive'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- face_images: the 5 registered face pictures of each student (JPEG bytes)
-- Deleting a student also deletes these rows (ON DELETE CASCADE).
-- ---------------------------------------------------------------------
CREATE TABLE `face_images` (
  `face_image_id` INT NOT NULL AUTO_INCREMENT,
  `student_id` INT NOT NULL,
  `image_number` TINYINT NOT NULL,              -- 1 to 5
  `image_data` MEDIUMBLOB NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`face_image_id`),
  UNIQUE KEY `uq_face_images_student_number` (`student_id`, `image_number`),
  CONSTRAINT `fk_face_images_student` FOREIGN KEY (`student_id`)
    REFERENCES `students` (`student_id`) ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT `chk_face_images_number` CHECK (`image_number` BETWEEN 1 AND 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- attendance: one row per student per day (the UNIQUE key blocks duplicates)
-- Students without a row for a date are shown as "Absent" by the program.
-- ---------------------------------------------------------------------
CREATE TABLE `attendance` (
  `attendance_id` INT NOT NULL AUTO_INCREMENT,
  `student_id` INT NOT NULL,
  `attendance_date` DATE NOT NULL,
  `time_in` TIME NOT NULL,
  `status` VARCHAR(10) NOT NULL DEFAULT 'Present',
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`attendance_id`),
  UNIQUE KEY `uq_attendance_student_date` (`student_id`, `attendance_date`),
  KEY `idx_attendance_date` (`attendance_date`),
  CONSTRAINT `fk_attendance_student` FOREIGN KEY (`student_id`)
    REFERENCES `students` (`student_id`) ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT `chk_attendance_status` CHECK (`status` IN ('Present'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ---------------------------------------------------------------------
-- Starting data
-- ---------------------------------------------------------------------
-- Default login: admin / admin123 (the password is stored as a BCrypt hash)
INSERT INTO `accounts` (`username`, `password`) VALUES
('admin', '$2a$11$ImieFcUoeFA4gmZBkIB1m.TAvs7tQi5eExMCNZ0s4ZV0CcaFKxmfa');

INSERT INTO `departments` (`department_code`, `department_name`, `department_head`, `contact_email`, `description`, `academic_level`) VALUES
('BSIT', 'Bachelor of Science in Information Technology', 'Dr. Maria Santos', 'bsit@example.com', 'Computer, programming and networking courses', 'Undergraduate'),
('BSBA', 'Bachelor of Science in Business Administration', 'Dr. Jose Ramirez', 'bsba@example.com', 'Business and management courses', 'Undergraduate'),
('BEED', 'Bachelor of Elementary Education', 'Dr. Ana Reyes', 'beed@example.com', 'Teacher training for elementary schools', 'Undergraduate'),
('BSED', 'Bachelor of Secondary Education', 'Dr. Pedro Cruz', 'bsed@example.com', 'Teacher training for high schools', 'Undergraduate');
