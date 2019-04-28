/*
SQLyog Community v12.09 (64 bit)
MySQL - 10.1.37-MariaDB : Database - sapphire
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
USE `sapphire`;

/*Table structure for table `devicetype` */

DROP TABLE IF EXISTS `devicetype`;

CREATE TABLE `devicetype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Table structure for table `location` */

DROP TABLE IF EXISTS `location`;

CREATE TABLE `location` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Latitude` double DEFAULT NULL,
  `Longitude` double DEFAULT NULL,
  `Country` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Location_Name` (`Name`),
  KEY `IX_Location_Country` (`Country`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Table structure for table `sensortype` */

DROP TABLE IF EXISTS `sensortype`;

CREATE TABLE `sensortype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
