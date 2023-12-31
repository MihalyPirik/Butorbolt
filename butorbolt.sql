﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 9.2.105.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 2023.11.29 10:33:16
-- Server version: 10.4.28
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE butorbolt;

--
-- Drop table `butor`
--
DROP TABLE IF EXISTS butor;

--
-- Drop table `alapanyag`
--
DROP TABLE IF EXISTS alapanyag;

--
-- Set default database
--
USE butorbolt;

--
-- Create table `alapanyag`
--
CREATE TABLE alapanyag (
  id INT(11) NOT NULL,
  megnevezes VARCHAR(255) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create table `butor`
--
CREATE TABLE butor (
  id INT(11) NOT NULL AUTO_INCREMENT,
  megnevezes VARCHAR(255) NOT NULL,
  ar INT(11) DEFAULT NULL,
  szin VARCHAR(255) NOT NULL,
  szallitas DATETIME DEFAULT NULL,
  alapanyag_id INT(11) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 9,
AVG_ROW_LENGTH = 6553,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE butor 
  ADD CONSTRAINT FK_butor_alapanyag_id FOREIGN KEY (alapanyag_id)
    REFERENCES alapanyag(id) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- 
-- Dumping data for table alapanyag
--
INSERT INTO alapanyag VALUES
(1, 'fa'),
(2, 'műanyag');

-- 
-- Dumping data for table butor
--
INSERT INTO butor VALUES
(1, 'szekrény', 25000, 'fekete', '2023-11-22 00:00:00', 1),
(2, 'komód', 12000, 'barna', '2023-11-24 00:00:00', 1),
(4, 'szék', 11000, 'fekete', '2023-11-04 00:00:00', 1),
(6, 'polc', 5000, 'fekete', '2023-11-04 00:00:00', 1),
(8, 'asztal', 20000, 'fekete', '2023-11-10 00:00:00', 2);

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;