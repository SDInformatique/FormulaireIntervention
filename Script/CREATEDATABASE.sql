-- --------------------------------------------------------
-- Hôte :                        127.0.0.1
-- Version du serveur:           10.4.10-MariaDB - mariadb.org binary distribution
-- SE du serveur:                Win64
-- HeidiSQL Version:             10.3.0.5771
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Listage de la structure de la base pour formulaireintervention
CREATE DATABASE IF NOT EXISTS `formulaireintervention` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `formulaireintervention`;

-- Listage de la structure de la table formulaireintervention. clients
CREATE TABLE IF NOT EXISTS `clients` (
  `ClientID` int(11) NOT NULL AUTO_INCREMENT,
  `ClientFirstName` varchar(50) DEFAULT NULL,
  `ClientLastName` varchar(50) DEFAULT NULL,
  `ClientAddress` varchar(50) DEFAULT NULL,
  `ClientPhoneNumber` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ClientID`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.

-- Listage de la structure de la table formulaireintervention. clientsignature
CREATE TABLE IF NOT EXISTS `clientsignature` (
  `signatureID` int(11) NOT NULL AUTO_INCREMENT,
  `clientID` int(11) DEFAULT NULL,
  `interventionID` int(11) DEFAULT NULL,
  `signatureData` text DEFAULT NULL,
  PRIMARY KEY (`signatureID`),
  KEY `FK__clients` (`clientID`),
  KEY `FK__intervention` (`interventionID`),
  CONSTRAINT `FK__clients` FOREIGN KEY (`clientID`) REFERENCES `clients` (`ClientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK__intervention` FOREIGN KEY (`interventionID`) REFERENCES `intervention` (`InterventionID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.

-- Listage de la structure de la table formulaireintervention. intervenant
CREATE TABLE IF NOT EXISTS `intervenant` (
  `IntervenantID` int(11) NOT NULL AUTO_INCREMENT,
  `IntervenantFirstName` varchar(50) NOT NULL DEFAULT '',
  `IntervenantLastName` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`IntervenantID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.

-- Listage de la structure de la table formulaireintervention. intervention
CREATE TABLE IF NOT EXISTS `intervention` (
  `InterventionID` int(11) NOT NULL AUTO_INCREMENT,
  `InterventionIntervenantID` int(11) NOT NULL,
  `InterventionClientID` int(11) NOT NULL,
  `InterventionTypeID` int(11) NOT NULL,
  `InterventionDuration` int(11) NOT NULL,
  PRIMARY KEY (`InterventionID`),
  KEY `FK_intervention_intervenant` (`InterventionIntervenantID`),
  KEY `FK_intervention_clients` (`InterventionClientID`),
  KEY `FK_intervention_interventiontype` (`InterventionTypeID`),
  CONSTRAINT `FK_intervention_clients` FOREIGN KEY (`InterventionClientID`) REFERENCES `clients` (`ClientID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_intervention_intervenant` FOREIGN KEY (`InterventionIntervenantID`) REFERENCES `intervenant` (`IntervenantID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_intervention_interventiontype` FOREIGN KEY (`InterventionTypeID`) REFERENCES `interventiontype` (`InterventionTypeID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.

-- Listage de la structure de la table formulaireintervention. interventiontype
CREATE TABLE IF NOT EXISTS `interventiontype` (
  `InterventionTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `InterventionTypeName` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`InterventionTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.

-- Listage de la structure de la vue formulaireintervention. view_intervention
-- Création d'une table temporaire pour palier aux erreurs de dépendances de VIEW
CREATE TABLE `view_intervention` (
	`InterventionID` INT(11) NOT NULL,
	`ClientFirstName` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`ClientLastName` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`ClientAddress` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`ClientPhoneNumber` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`IntervenantFirstName` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`IntervenantLastName` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`InterventionTypeName` VARCHAR(50) NULL COLLATE 'latin1_swedish_ci',
	`InterventionDuration` INT(11) NOT NULL
) ENGINE=InnoDB;

-- Listage de la structure de la vue formulaireintervention. view_intervention
-- Suppression de la table temporaire et création finale de la structure d'une vue
DROP TABLE IF EXISTS `view_intervention`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `view_intervention` AS select `intervention`.`InterventionID` AS `InterventionID`,`clients`.`ClientFirstName` AS `ClientFirstName`,`clients`.`ClientLastName` AS `ClientLastName`,`clients`.`ClientAddress` AS `ClientAddress`,`clients`.`ClientPhoneNumber` AS `ClientPhoneNumber`,`intervenant`.`IntervenantFirstName` AS `IntervenantFirstName`,`intervenant`.`IntervenantLastName` AS `IntervenantLastName`,`interventiontype`.`InterventionTypeName` AS `InterventionTypeName`,`intervention`.`InterventionDuration` AS `InterventionDuration` from (((`intervention` left join `clients` on(`intervention`.`InterventionClientID` = `clients`.`ClientID`)) left join `intervenant` on(`intervention`.`InterventionIntervenantID` = `intervenant`.`IntervenantID`)) left join `interventiontype` on(`intervention`.`InterventionTypeID` = `interventiontype`.`InterventionTypeID`));

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

CREATE USER 'DBconnector'@'localhost' IDENTIFIED BY 'Pa$$w0rd';
GRANT USAGE ON *.* TO 'DBconnector'@'localhost';
GRANT EXECUTE, SELECT, SHOW VIEW, ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, INDEX, INSERT, REFERENCES, TRIGGER, UPDATE, LOCK TABLES  ON `formulaireintervention`.* TO 'test'@'localhost' WITH GRANT OPTION;


