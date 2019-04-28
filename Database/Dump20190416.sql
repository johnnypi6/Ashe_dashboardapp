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

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `aspnetroleclaims` */

DROP TABLE IF EXISTS `aspnetroleclaims`;

CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `aspnetuserclaims` */

DROP TABLE IF EXISTS `aspnetuserclaims`;

CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `aspnetuserlogins` */

DROP TABLE IF EXISTS `aspnetuserlogins`;

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `aspnetuserroles` */

DROP TABLE IF EXISTS `aspnetuserroles`;

CREATE TABLE `aspnetuserroles` (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `aspnetusertokens` */

DROP TABLE IF EXISTS `aspnetusertokens`;

CREATE TABLE `aspnetusertokens` (
  `UserId` int(11) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `device` */

DROP TABLE IF EXISTS `device`;

CREATE TABLE `device` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `LocationId` int(11) NOT NULL,
  `IMEI` varchar(256) DEFAULT NULL,
  `SIMCard` varchar(256) DEFAULT NULL,
  `DeviceTypeId` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Device_UserId` (`UserId`),
  KEY `IX_Device_LocationId` (`LocationId`),
  KEY `IX_Device_DeviceTypeId` (`DeviceTypeId`),
  CONSTRAINT `FK_Device_DeviceTypeId` FOREIGN KEY (`DeviceTypeId`) REFERENCES `devicetype` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Device_Location_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `location` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Device_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `Id` int(11) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `DisplayName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `sensor` */

DROP TABLE IF EXISTS `sensor`;

CREATE TABLE `sensor` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DeviceId` int(11) NOT NULL,
  `SensorTypeId` int(11) NOT NULL,
  `SerialNumber` varchar(256) DEFAULT NULL,
  `HighThreshold` double DEFAULT NULL,
  `LowThreshold` double DEFAULT NULL,
  `RelayOperation` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sensor_DeviceId` (`DeviceId`),
  KEY `IX_Sensor_SensorTypeId` (`SensorTypeId`),
  CONSTRAINT `FK_Sensor_Device_Id` FOREIGN KEY (`DeviceId`) REFERENCES `device` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Sensor_SensorType_Id` FOREIGN KEY (`SensorTypeId`) REFERENCES `sensortype` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

/*Table structure for table `sensortype` */

DROP TABLE IF EXISTS `sensortype`;

CREATE TABLE `sensortype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(256) NOT NULL,
  `NormalizedUserName` varchar(256) NOT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `Phone` varchar(256) DEFAULT NULL,
  `Mobile` varchar(256) DEFAULT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `Company` varchar(256) DEFAULT NULL,
  `ContactPerson` varchar(256) DEFAULT NULL,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  UNIQUE KEY `UserEmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
