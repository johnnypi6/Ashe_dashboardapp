/*
SQLyog Ultimate v12.4.3 (64 bit)
MySQL - 10.1.38-MariaDB : Database - sapphire
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`sapphire` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `sapphire`;

/*Table structure for table `device` */

DROP TABLE IF EXISTS `device`;

CREATE TABLE `device` (
  `id` int(128) NOT NULL AUTO_INCREMENT,
  `user_id` int(123) DEFAULT NULL,
  `location_id` int(128) DEFAULT NULL,
  `IMEI` varchar(256) DEFAULT NULL,
  `sim_card` varchar(256) DEFAULT NULL,
  `type` int(128) DEFAULT NULL,
  `vehicle` varchar(256) DEFAULT NULL,
  `status` int(128) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `device` */

/*Table structure for table `location` */

DROP TABLE IF EXISTS `location`;

CREATE TABLE `location` (
  `id` int(128) NOT NULL AUTO_INCREMENT,
  `name` varchar(256) DEFAULT NULL,
  `latitude` varchar(256) DEFAULT NULL,
  `longitude` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `location` */

/*Table structure for table `sensor` */

DROP TABLE IF EXISTS `sensor`;

CREATE TABLE `sensor` (
  `id` int(128) NOT NULL AUTO_INCREMENT,
  `device_id` int(128) DEFAULT NULL,
  `type` int(128) DEFAULT NULL,
  `serial_number` int(128) DEFAULT NULL,
  `high_threshold` decimal(65,0) DEFAULT NULL,
  `low_threshold` decimal(65,0) DEFAULT NULL,
  `relay_operation` int(128) DEFAULT NULL,
  `status` int(128) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `sensor` */

insert  into `sensor`(`id`,`device_id`,`type`,`serial_number`,`high_threshold`,`low_threshold`,`relay_operation`,`status`) values 
(1,1,0,12345,23,32,1,1);

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int(16) NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL,
  `password` varchar(32) NOT NULL,
  `email` varchar(32) NOT NULL,
  `phone` varchar(32) DEFAULT NULL,
  `mobile` varchar(32) DEFAULT NULL,
  `address` varchar(128) DEFAULT NULL,
  `company` varchar(128) DEFAULT NULL,
  `contactperson` varchar(256) DEFAULT NULL,
  `role` enum('admin','superadmin','user') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `user` */

insert  into `user`(`id`,`name`,`password`,`email`,`phone`,`mobile`,`address`,`company`,`contactperson`,`role`) values 
(1,'paulo','admin821116','paulo82116@outlook.com','1953501668','1913501668','Hong Kong','UITC','xiao','user'),
(2,'xiao','11111','xiao114@hotmail.com','1953501668','1913501668','Hong Kong','UITC','paulo','user');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
