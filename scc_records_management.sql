-- SCC Records Management System
-- Import in phpMyAdmin after starting MySQL/MariaDB in XAMPP.
-- WARNING: re-importing drops and recreates this database and removes its data.
-- Default administrator: admin / admin123

CREATE DATABASE IF NOT EXISTS `scc_records_management`
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `scc_records_management`;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `obligation_adjustments`;
DROP TABLE IF EXISTS `event_attendance`;
DROP TABLE IF EXISTS `event_sessions`;
DROP TABLE IF EXISTS `attendance_events`;
DROP TABLE IF EXISTS `face_images`;
DROP TABLE IF EXISTS `officers`;
DROP TABLE IF EXISTS `students`;
DROP TABLE IF EXISTS `departments`;
DROP TABLE IF EXISTS `password_resets`;
DROP TABLE IF EXISTS `accounts`;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE `accounts` (
  `account_id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(50) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `role` VARCHAR(20) NOT NULL DEFAULT 'Admin',
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`account_id`),
  UNIQUE KEY `uq_accounts_username` (`username`),
  CONSTRAINT `chk_accounts_role` CHECK (`role` IN ('Admin', 'Officer'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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

CREATE TABLE `students` (
  `student_id` INT NOT NULL AUTO_INCREMENT,
  `student_number` VARCHAR(20) NOT NULL,
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
  UNIQUE KEY `uq_students_email` (`email`),
  KEY `idx_students_name` (`last_name`, `first_name`),
  KEY `idx_students_department` (`department_id`),
  CONSTRAINT `fk_students_department` FOREIGN KEY (`department_id`)
    REFERENCES `departments` (`department_id`) ON UPDATE CASCADE ON DELETE RESTRICT,
  CONSTRAINT `chk_students_status` CHECK (`status` IN ('Active', 'Inactive'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `officers` (
  `officer_id` INT NOT NULL AUTO_INCREMENT,
  `account_id` INT NOT NULL,
  `officer_number` VARCHAR(20) NOT NULL,
  `first_name` VARCHAR(50) NOT NULL,
  `middle_name` VARCHAR(50) DEFAULT NULL,
  `last_name` VARCHAR(50) NOT NULL,
  `department_id` INT NOT NULL,
  `year_level` VARCHAR(20) NOT NULL,
  `gender` VARCHAR(10) NOT NULL,
  `email` VARCHAR(100) DEFAULT NULL,
  `contact_number` VARCHAR(20) DEFAULT NULL,
  `position` VARCHAR(60) NOT NULL,
  `school_year` VARCHAR(20) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`officer_id`),
  UNIQUE KEY `uq_officers_account` (`account_id`),
  UNIQUE KEY `uq_officers_number` (`officer_number`),
  KEY `idx_officers_name` (`last_name`, `first_name`),
  CONSTRAINT `fk_officers_account` FOREIGN KEY (`account_id`)
    REFERENCES `accounts` (`account_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_officers_department` FOREIGN KEY (`department_id`)
    REFERENCES `departments` (`department_id`) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `face_images` (
  `face_image_id` INT NOT NULL AUTO_INCREMENT,
  `student_id` INT NOT NULL,
  `image_number` TINYINT NOT NULL,
  `image_data` MEDIUMBLOB NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`face_image_id`),
  UNIQUE KEY `uq_face_images_student_number` (`student_id`, `image_number`),
  CONSTRAINT `fk_face_images_student` FOREIGN KEY (`student_id`)
    REFERENCES `students` (`student_id`) ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT `chk_face_images_number` CHECK (`image_number` BETWEEN 1 AND 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `attendance_events` (
  `event_id` INT NOT NULL AUTO_INCREMENT,
  `event_name` VARCHAR(120) NOT NULL,
  `venue` VARCHAR(120) DEFAULT NULL,
  `start_date` DATE NOT NULL,
  `end_date` DATE NOT NULL,
  `department_id` INT DEFAULT NULL,
  `requires_logout` TINYINT(1) NOT NULL DEFAULT 0,
  `morning_enabled` TINYINT(1) NOT NULL DEFAULT 1,
  `morning_start` TIME NOT NULL DEFAULT '08:00:00',
  `morning_end` TIME NOT NULL DEFAULT '12:00:00',
  `afternoon_enabled` TINYINT(1) NOT NULL DEFAULT 1,
  `afternoon_start` TIME NOT NULL DEFAULT '13:00:00',
  `afternoon_end` TIME NOT NULL DEFAULT '17:00:00',
  `created_by` INT DEFAULT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`event_id`),
  KEY `idx_events_dates` (`start_date`, `end_date`),
  KEY `idx_events_department` (`department_id`),
  CONSTRAINT `fk_events_department` FOREIGN KEY (`department_id`)
    REFERENCES `departments` (`department_id`) ON UPDATE CASCADE ON DELETE SET NULL,
  CONSTRAINT `fk_events_creator` FOREIGN KEY (`created_by`)
    REFERENCES `accounts` (`account_id`) ON DELETE SET NULL,
  CONSTRAINT `chk_events_dates` CHECK (`end_date` >= `start_date`),
  CONSTRAINT `chk_events_morning` CHECK (`morning_end` > `morning_start`),
  CONSTRAINT `chk_events_afternoon` CHECK (`afternoon_end` > `afternoon_start`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `event_sessions` (
  `session_id` INT NOT NULL AUTO_INCREMENT,
  `event_id` INT NOT NULL,
  `session_date` DATE NOT NULL,
  `session_name` VARCHAR(20) NOT NULL,
  `start_time` TIME NOT NULL,
  `end_time` TIME NOT NULL,
  `enabled` TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`session_id`),
  UNIQUE KEY `uq_event_session` (`event_id`, `session_date`, `session_name`),
  KEY `idx_sessions_schedule` (`session_date`, `start_time`, `end_time`),
  CONSTRAINT `fk_sessions_event` FOREIGN KEY (`event_id`)
    REFERENCES `attendance_events` (`event_id`) ON DELETE CASCADE,
  CONSTRAINT `chk_session_name` CHECK (`session_name` IN ('Morning', 'Afternoon')),
  CONSTRAINT `chk_session_times` CHECK (`end_time` > `start_time`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `event_attendance` (
  `event_attendance_id` INT NOT NULL AUTO_INCREMENT,
  `session_id` INT NOT NULL,
  `student_id` INT NOT NULL,
  `time_in` DATETIME NOT NULL,
  `time_out` DATETIME DEFAULT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`event_attendance_id`),
  UNIQUE KEY `uq_attendance_session_student` (`session_id`, `student_id`),
  KEY `idx_event_attendance_student` (`student_id`),
  CONSTRAINT `fk_event_attendance_session` FOREIGN KEY (`session_id`)
    REFERENCES `event_sessions` (`session_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_event_attendance_student` FOREIGN KEY (`student_id`)
    REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE `obligation_adjustments` (
  `adjustment_id` INT NOT NULL AUTO_INCREMENT,
  `student_id` INT NOT NULL,
  `event_id` INT NOT NULL,
  `points_deducted` INT NOT NULL,
  `reason` VARCHAR(255) NOT NULL,
  `account_id` INT DEFAULT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`adjustment_id`),
  KEY `idx_adjustments_student` (`student_id`, `created_at`),
  KEY `idx_adjustments_event` (`event_id`, `student_id`),
  CONSTRAINT `fk_adjustments_student` FOREIGN KEY (`student_id`)
    REFERENCES `students` (`student_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_adjustments_event` FOREIGN KEY (`event_id`)
    REFERENCES `attendance_events` (`event_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_adjustments_account` FOREIGN KEY (`account_id`)
    REFERENCES `accounts` (`account_id`) ON DELETE SET NULL,
  CONSTRAINT `chk_adjustment_points` CHECK (`points_deducted` > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `accounts` (`username`, `password`, `role`) VALUES
('admin', '$2a$11$ImieFcUoeFA4gmZBkIB1m.TAvs7tQi5eExMCNZ0s4ZV0CcaFKxmfa', 'Admin');

INSERT INTO `departments` (`department_code`, `department_name`, `department_head`, `contact_email`, `description`, `academic_level`) VALUES
('BSIT', 'Bachelor of Science in Information Technology', 'Dr. Maria Santos', 'bsit@example.com', 'Computer, programming and networking courses', 'Undergraduate'),
('BSBA', 'Bachelor of Science in Business Administration', 'Dr. Jose Ramirez', 'bsba@example.com', 'Business and management courses', 'Undergraduate'),
('BEED', 'Bachelor of Elementary Education', 'Dr. Ana Reyes', 'beed@example.com', 'Teacher training for elementary courses', 'Undergraduate'),
('BSED', 'Bachelor of Secondary Education', 'Dr. Pedro Cruz', 'bsed@example.com', 'Teacher training for high schools', 'Undergraduate');
