/*
SQLyog Community v12.09 (64 bit)
MySQL - 10.1.37-MariaDB : Database - webshare
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`webshare` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `webshare`;

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `__efmigrationshistory` */

insert  into `__efmigrationshistory`(`MigrationId`,`ProductVersion`) values ('00000000000000_CreateIdentitySchema','2.2.3-servicing-35854');

/*Table structure for table `aspnetroleclaims` */

DROP TABLE IF EXISTS `aspnetroleclaims`;

CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetroleclaims` */

/*Table structure for table `aspnetroles` */

DROP TABLE IF EXISTS `aspnetroles`;

CREATE TABLE `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetroles` */

/*Table structure for table `aspnetuserclaims` */

DROP TABLE IF EXISTS `aspnetuserclaims`;

CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserclaims` */

/*Table structure for table `aspnetuserlogins` */

DROP TABLE IF EXISTS `aspnetuserlogins`;

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserlogins` */

/*Table structure for table `aspnetuserroles` */

DROP TABLE IF EXISTS `aspnetuserroles`;

CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetuserroles` */

/*Table structure for table `aspnetusers` */

DROP TABLE IF EXISTS `aspnetusers`;

CREATE TABLE `aspnetusers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `ConcurrencyStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetusers` */

/*Table structure for table `aspnetusertokens` */

DROP TABLE IF EXISTS `aspnetusertokens`;

CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `aspnetusertokens` */

/*Table structure for table `course` */

DROP TABLE IF EXISTS `course`;

CREATE TABLE `course` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `course` */

/*Table structure for table `coursetopic` */

DROP TABLE IF EXISTS `coursetopic`;

CREATE TABLE `coursetopic` (
  `CourseId` int(11) NOT NULL,
  `TopicId` int(11) NOT NULL,
  KEY `CourseTopic_CourseId` (`CourseId`),
  KEY `CourseTopic_TopicId` (`TopicId`),
  CONSTRAINT `FK_CourseTopic_Course_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `course` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CourseTopic_Topic_TopicId` FOREIGN KEY (`TopicId`) REFERENCES `topic` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `coursetopic` */

/*Table structure for table `topic` */

DROP TABLE IF EXISTS `topic`;

CREATE TABLE `topic` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `topic` */

/*Table structure for table `url` */

DROP TABLE IF EXISTS `url`;

CREATE TABLE `url` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TopicId` int(11) NOT NULL,
  `URLString` longtext,
  `UploaderId` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_URL_AspNetUsers_UserId` (`UploaderId`),
  KEY `TopicIndex` (`TopicId`),
  CONSTRAINT `FK_URL_AspNetUsers_UserId` FOREIGN KEY (`UploaderId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_URL_Topic_TopicId` FOREIGN KEY (`TopicId`) REFERENCES `topic` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `url` */

/*Table structure for table `usercourse` */

DROP TABLE IF EXISTS `usercourse`;

CREATE TABLE `usercourse` (
  `UserId` varchar(255) NOT NULL,
  `CourseId` int(11) NOT NULL,
  KEY `UserCourse_UserId` (`UserId`),
  KEY `UserCourse_CourseId` (`CourseId`),
  CONSTRAINT `FK_UserCourse_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_UserCourse_Course_CourseId` FOREIGN KEY (`CourseId`) REFERENCES `course` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `usercourse` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
