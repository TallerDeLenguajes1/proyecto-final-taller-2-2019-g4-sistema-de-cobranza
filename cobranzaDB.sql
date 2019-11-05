-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cobranza
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `deuda`
--

DROP TABLE IF EXISTS `deuda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deuda` (
  `dni` varchar(10) NOT NULL,
  `cuit` varchar(15) NOT NULL,
  `monto` double DEFAULT NULL,
  PRIMARY KEY (`dni`,`cuit`),
  KEY `fkCuitDeuda_idx` (`cuit`),
  CONSTRAINT `fkCuitDeuda` FOREIGN KEY (`cuit`) REFERENCES `empresa` (`cuit`) ON UPDATE CASCADE,
  CONSTRAINT `fkDniDeuda` FOREIGN KEY (`dni`) REFERENCES `deudor` (`dni`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deuda`
--

LOCK TABLES `deuda` WRITE;
/*!40000 ALTER TABLE `deuda` DISABLE KEYS */;
/*!40000 ALTER TABLE `deuda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deudor`
--

DROP TABLE IF EXISTS `deudor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `deudor` (
  `dni` varchar(10) NOT NULL,
  `ApellidoNombre` varchar(60) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`dni`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deudor`
--

LOCK TABLES `deudor` WRITE;
/*!40000 ALTER TABLE `deudor` DISABLE KEYS */;
/*!40000 ALTER TABLE `deudor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empresa`
--

DROP TABLE IF EXISTS `empresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empresa` (
  `cuit` varchar(15) NOT NULL,
  `nombre` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`cuit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empresa`
--

LOCK TABLES `empresa` WRITE;
/*!40000 ALTER TABLE `empresa` DISABLE KEYS */;
/*!40000 ALTER TABLE `empresa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registro`
--

DROP TABLE IF EXISTS `registro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `registro` (
  `id_registro` int(11) NOT NULL,
  `fechahora` datetime DEFAULT NULL,
  `observacion` varchar(45) DEFAULT NULL,
  `resultado` varchar(15) DEFAULT NULL,
  `dni` varchar(10) DEFAULT NULL,
  `cuit` varchar(15) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_registro`),
  KEY `fkDniRegistro_idx` (`dni`),
  KEY `fkCuitRegistro_idx` (`cuit`),
  KEY `fkUsuarioRegistro_idx` (`id_usuario`),
  CONSTRAINT `fkCuitRegistro` FOREIGN KEY (`cuit`) REFERENCES `empresa` (`cuit`),
  CONSTRAINT `fkDniRegistro` FOREIGN KEY (`dni`) REFERENCES `deudor` (`dni`),
  CONSTRAINT `fkUsuarioRegistro` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registro`
--

LOCK TABLES `registro` WRITE;
/*!40000 ALTER TABLE `registro` DISABLE KEYS */;
/*!40000 ALTER TABLE `registro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `user` varchar(45) DEFAULT NULL,
  `contraseña` varchar(45) DEFAULT NULL,
  `nivel` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `user_UNIQUE` (`user`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-04 21:15:42
