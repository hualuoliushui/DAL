-- MySQL dump 10.13  Distrib 5.7.15, for Win64 (x86_64)
--
-- Host: localhost    Database: testdal
-- ------------------------------------------------------
-- Server version	5.7.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `agenda`
--

DROP TABLE IF EXISTS `agenda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `agenda` (
  `agendaID` int(32) NOT NULL,
  `agendaName` varchar(255) NOT NULL,
  `agendaIndex` int(32) NOT NULL,
  `agendaDuration` int(32) DEFAULT '0',
  `meetingID` int(32) NOT NULL,
  `personID` int(32) NOT NULL,
  `isUpdate` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`agendaID`),
  KEY `FK_AGENDA_MEETINGID` (`meetingID`),
  KEY `FK_AGENDA_PERSONID` (`personID`),
  CONSTRAINT `FK_AGENDA_MEETINGID` FOREIGN KEY (`meetingID`) REFERENCES `meeting` (`meetingID`),
  CONSTRAINT `FK_AGENDA_PERSONID` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `delegate`
--

DROP TABLE IF EXISTS `delegate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `delegate` (
  `delegateID` int(32) NOT NULL,
  `isSignIn` tinyint(1) DEFAULT '0',
  `personMeetingRole` int(32) DEFAULT '0',
  `deviceID` int(32) NOT NULL,
  `meetingID` int(32) NOT NULL,
  `personID` int(32) NOT NULL,
  `isUpdate` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`delegateID`),
  KEY `FK_DELEGATE_DEVICEID` (`deviceID`),
  KEY `FK_DELEGATE_MEETINGID` (`meetingID`),
  KEY `FD_DELEGATE_PERSONID` (`personID`),
  CONSTRAINT `FD_DELEGATE_PERSONID` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`),
  CONSTRAINT `FK_DELEGATE_DEVICEID` FOREIGN KEY (`deviceID`) REFERENCES `device` (`deviceID`),
  CONSTRAINT `FK_DELEGATE_MEETINGID` FOREIGN KEY (`meetingID`) REFERENCES `meeting` (`meetingID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `device`
--

DROP TABLE IF EXISTS `device`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `device` (
  `deviceID` int(32) NOT NULL,
  `IMEI` varchar(255) NOT NULL,
  `deviceIndex` int(32) NOT NULL,
  `deviceState` int(32) DEFAULT '0',
  PRIMARY KEY (`deviceID`),
  UNIQUE KEY `IMEI` (`IMEI`),
  UNIQUE KEY `deviceIndex` (`deviceIndex`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `file`
--

DROP TABLE IF EXISTS `file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `file` (
  `fileID` int(32) NOT NULL,
  `fileName` varchar(255) NOT NULL,
  `fileIndex` int(32) NOT NULL,
  `fileSize` int(32) NOT NULL,
  `filePath` varchar(255) NOT NULL,
  `agendaID` int(32) NOT NULL,
  `isUpdate` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`fileID`),
  UNIQUE KEY `filePath` (`filePath`),
  KEY `FK_FILE_AGENDAID` (`agendaID`),
  CONSTRAINT `FK_FILE_AGENDAID` FOREIGN KEY (`agendaID`) REFERENCES `agenda` (`agendaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `meeting`
--

DROP TABLE IF EXISTS `meeting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meeting` (
  `meetingID` int(32) NOT NULL,
  `meetingName` varchar(255) NOT NULL,
  `meetingSummary` varchar(255) DEFAULT '',
  `meetingDuration` int(32) DEFAULT '0',
  `meetingToStartTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `meetingStartedTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `meetingStatus` int(32) DEFAULT '1',
  `delegateUpdateStatus` int(32) DEFAULT '0',
  `agendaUpdateStatus` int(32) DEFAULT '0',
  `fileUpdateStatus` int(32) DEFAULT '0',
  `voteUpdateStatus` int(32) DEFAULT '0',
  `meetingPlaceID` int(32) NOT NULL,
  `personID` int(32) NOT NULL,
  PRIMARY KEY (`meetingID`),
  UNIQUE KEY `meetingName` (`meetingName`),
  KEY `FK_MEETING_MEETINGPLACEID` (`meetingPlaceID`),
  KEY `FK_MEETING_PERSONID` (`personID`),
  CONSTRAINT `FK_MEETING_MEETINGPLACEID` FOREIGN KEY (`meetingPlaceID`) REFERENCES `meetingplace` (`meetingPlaceID`),
  CONSTRAINT `FK_MEETING_PERSONID` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `meetingplace`
--

DROP TABLE IF EXISTS `meetingplace`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `meetingplace` (
  `meetingPlaceID` int(32) NOT NULL,
  `meetingPlaceName` varchar(255) NOT NULL,
  `meetingPlaceCapacity` varchar(255) DEFAULT '0',
  `meetingPlaceState` int(32) DEFAULT '0',
  PRIMARY KEY (`meetingPlaceID`),
  UNIQUE KEY `meetingPlaceName` (`meetingPlaceName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `permission`
--

DROP TABLE IF EXISTS `permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `permission` (
  `permissionID` int(32) NOT NULL,
  `permissionName` varchar(255) NOT NULL,
  `permissionDescription` varchar(255) NOT NULL,
  PRIMARY KEY (`permissionID`),
  UNIQUE KEY `permissionName` (`permissionName`),
  UNIQUE KEY `permissionDescription` (`permissionDescription`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `person` (
  `personID` int(32) NOT NULL,
  `personName` varchar(255) NOT NULL,
  `personPassword` varchar(255) NOT NULL,
  `personDepartment` varchar(255) NOT NULL,
  `personJob` varchar(255) NOT NULL,
  `personDescription` varchar(255) DEFAULT NULL,
  `personState` int(32) DEFAULT '0',
  `isAdmin` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`personID`),
  UNIQUE KEY `personName` (`personName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `person_role`
--

DROP TABLE IF EXISTS `person_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `person_role` (
  `person_roleID` int(32) NOT NULL,
  `personID` int(32) NOT NULL,
  `roleID` int(32) NOT NULL,
  PRIMARY KEY (`person_roleID`),
  KEY `FK_PERSON_ROLE_PERSONID` (`personID`),
  KEY `FK_PERSON_ROLE_ROLEID` (`roleID`),
  CONSTRAINT `FK_PERSON_ROLE_PERSONID` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`),
  CONSTRAINT `FK_PERSON_ROLE_ROLEID` FOREIGN KEY (`roleID`) REFERENCES `role` (`roleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `roleID` int(32) NOT NULL,
  `roleName` varchar(255) NOT NULL,
  `isIntegrant` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`roleID`),
  UNIQUE KEY `roleName` (`roleName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `role_permission`
--

DROP TABLE IF EXISTS `role_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_permission` (
  `role_permissionID` int(32) NOT NULL,
  `roleID` int(32) NOT NULL,
  `permissionID` int(32) NOT NULL,
  PRIMARY KEY (`role_permissionID`),
  KEY `FK_ROLE_PERMISSION_ROLEID` (`roleID`),
  KEY `FK_ROLE_PERMISSION_PERMISSIONID` (`permissionID`),
  CONSTRAINT `FK_ROLE_PERMISSION_PERMISSIONID` FOREIGN KEY (`permissionID`) REFERENCES `permission` (`permissionID`),
  CONSTRAINT `FK_ROLE_PERMISSION_ROLEID` FOREIGN KEY (`roleID`) REFERENCES `role` (`roleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `vote`
--

DROP TABLE IF EXISTS `vote`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vote` (
  `voteID` int(32) NOT NULL,
  `voteName` varchar(255) NOT NULL,
  `voteIndex` int(32) NOT NULL,
  `voteDescription` varchar(255) DEFAULT '',
  `voteType` int(32) NOT NULL,
  `voteStatus` int(32) DEFAULT '1',
  `agendaID` int(32) NOT NULL,
  `isUpdate` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`voteID`),
  KEY `FK_VOTE_AGENDAID` (`agendaID`),
  CONSTRAINT `FK_VOTE_AGENDAID` FOREIGN KEY (`agendaID`) REFERENCES `agenda` (`agendaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `voteoption`
--

DROP TABLE IF EXISTS `voteoption`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voteoption` (
  `voteOptionID` int(32) NOT NULL,
  `voteOptionName` varchar(255) NOT NULL,
  `voteOptionIndex` int(32) NOT NULL,
  `voteID` int(32) NOT NULL,
  PRIMARY KEY (`voteOptionID`),
  KEY `FK_VOTEOPTION_VOTEID` (`voteID`),
  CONSTRAINT `FK_VOTEOPTION_VOTEID` FOREIGN KEY (`voteID`) REFERENCES `vote` (`voteID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `voteoptionpersonresult`
--

DROP TABLE IF EXISTS `voteoptionpersonresult`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `voteoptionpersonresult` (
  `voteOptionPersonResultID` int(32) NOT NULL,
  `voteOptionID` int(32) NOT NULL,
  `personID` int(32) NOT NULL,
  PRIMARY KEY (`voteOptionPersonResultID`),
  KEY `FK_VOTEOPTIONPERSONRESULT_VOTEOPTIONID` (`voteOptionID`),
  KEY `FK_VOTEOPTIONPERSONRESULT_PERSONID` (`personID`),
  CONSTRAINT `FK_VOTEOPTIONPERSONRESULT_PERSONID` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`),
  CONSTRAINT `FK_VOTEOPTIONPERSONRESULT_VOTEOPTIONID` FOREIGN KEY (`voteOptionID`) REFERENCES `voteoption` (`voteOptionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-07 10:23:50
