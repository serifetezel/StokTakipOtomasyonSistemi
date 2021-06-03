-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: localhost    Database: stok_takip
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `sales`
--

DROP TABLE IF EXISTS `sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `sales` (
  `tc` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `namesurname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `telephone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `barcodeno` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `productname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `saleprice` decimal(18,2) DEFAULT NULL,
  `totalprice` decimal(18,2) DEFAULT NULL,
  `date` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales`
--

LOCK TABLES `sales` WRITE;
/*!40000 ALTER TABLE `sales` DISABLE KEYS */;
INSERT INTO `sales` VALUES ('','','','102','Çubuk Kraker',2,1.50,3.00,'21.12.2020 01:43:26'),('','','','102','Çubuk Kraker',2,1.50,3.00,'21.12.2020 01:43:26'),('','','','215','2.5 lt kola',1,4.10,4.10,'21.12.2020 01:43:26'),('','','','215','2.5 lt kola',1,4.10,4.10,'21.12.2020 01:43:26'),('','','','102','Çubuk Kraker',2,1.50,3.00,'21.12.2020 01:44:19'),('','','','102','Çubuk Kraker',2,1.50,3.00,'21.12.2020 01:44:19'),('','','','102','Çubuk Kraker',2,1.50,3.00,'21.12.2020 01:51:54'),('','','','215','2.5 lt kola',1,4.10,4.10,'21.12.2020 01:51:54'),('','','','100','Baldo',1,5.50,5.50,'25.12.2020 20:13:34'),('','','','103','Çekirdek',1,3.85,3.85,'25.12.2020 20:13:34'),('','','','104','Mango',3,8.00,24.00,'25.12.2020 20:13:34'),('11111111111','Ayşe Çevik','0 121 212 12 12','100','Baldo',2,5.50,11.00,'27.12.2020 15:20:06'),('11111111111','Ayşe Çevik','0 121 212 12 12','101','Vişne Suyu',1,1.75,1.75,'27.12.2020 15:20:06'),('11111111111','Ayşe Çevik','0 121 212 12 12','105','Karnabahar',1,3.00,3.00,'27.12.2020 15:20:06'),('11111111111','Ayşe Çevik','0 121 212 12 12','104','Mango',1,8.00,8.00,'27.12.2020 15:20:06'),('11111111111','Ayşe Çevik','0 121 212 12 12','103','Çekirdek',1,3.85,3.85,'27.12.2020 15:20:07'),('33333333333','Metin Demir','0 343 434 34 34','105','Karnabahar',1,3.00,3.00,'27.12.2020 20:18:20'),('33333333333','Metin Demir','0 343 434 34 34','103','Çekirdek',4,3.85,15.40,'27.12.2020 20:18:20'),('11111111111','Ayşe Çevik','0 121 212 12 12','103','Çekirdek',1,3.85,3.85,'28.12.2020 01:50:53'),('','','','103','Çekirdek',1,3.85,3.85,'29.12.2020 20:39:07'),('','','','105','Karnabahar',2,3.00,6.00,'29.12.2020 20:39:09'),('','','','104','Mango',1,8.00,8.00,'29.12.2020 20:39:10');
/*!40000 ALTER TABLE `sales` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-12-30 15:20:05
