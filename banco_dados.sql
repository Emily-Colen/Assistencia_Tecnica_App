-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: assistencia_tecnica
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.32-MariaDB

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
-- Table structure for table `bairro`
--

DROP TABLE IF EXISTS `bairro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bairro` (
  `idbairro` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `cidade_idcidade` int(11) NOT NULL,
  PRIMARY KEY (`idbairro`),
  KEY `fk_bairro_cidade` (`cidade_idcidade`),
  CONSTRAINT `fk_bairro_cidade` FOREIGN KEY (`cidade_idcidade`) REFERENCES `cidade` (`idcidade`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bairro`
--

LOCK TABLES `bairro` WRITE;
/*!40000 ALTER TABLE `bairro` DISABLE KEYS */;
INSERT INTO `bairro` VALUES (101,'Jardim Satélite',101),(102,'Centro',102),(103,'Urbanova',103);
/*!40000 ALTER TABLE `bairro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cidade`
--

DROP TABLE IF EXISTS `cidade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cidade` (
  `idcidade` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `estado_idestado` int(11) NOT NULL,
  PRIMARY KEY (`idcidade`),
  KEY `fk_cidade_estado` (`estado_idestado`),
  CONSTRAINT `fk_cidade_estado` FOREIGN KEY (`estado_idestado`) REFERENCES `estado` (`idestado`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cidade`
--

LOCK TABLES `cidade` WRITE;
/*!40000 ALTER TABLE `cidade` DISABLE KEYS */;
INSERT INTO `cidade` VALUES (101,'São José dos Campos',101),(102,'Pouso Alegre',102),(103,'Niterói',103);
/*!40000 ALTER TABLE `cidade` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cliente` (
  `idcliente` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `cpf_cnpj` varchar(18) NOT NULL,
  `email` varchar(70) NOT NULL,
  `telefone` varchar(20) NOT NULL,
  `rua_idrua` int(11) NOT NULL,
  `numero_endereco` varchar(45) DEFAULT NULL,
  `complemento` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idcliente`),
  UNIQUE KEY `uk_cliente_cpf_cnpj` (`cpf_cnpj`),
  KEY `fk_cliente_rua` (`rua_idrua`),
  CONSTRAINT `fk_cliente_rua` FOREIGN KEY (`rua_idrua`) REFERENCES `rua` (`idrua`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (101,'Carlos Henrique Silva','123.456.789-00','carlos@email.com','(12) 99999-1111',101,'150','Casa'),(102,'Mariana Souza LTDA','12.345.678/0001-99','contato@marianasouza.com','(35) 98888-2222',102,'900','Sala 12'),(103,'Fernanda Lima','987.654.321-00','fernanda@email.com','(21) 97777-3333',103,'45','Apto 302'),(104,'Pedro Silva','002.000.000-01','Pedro@teste.com','12980000000',101,'100',NULL),(105,'Pedro Silva ','000.000.000-01','Pedro@gmail.com','12981999999',101,'100',NULL);
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipamento`
--

DROP TABLE IF EXISTS `equipamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipamento` (
  `idequipamento` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` varchar(45) NOT NULL,
  `marca` varchar(45) NOT NULL,
  `modelo` varchar(45) NOT NULL,
  `numero_serie` varchar(45) NOT NULL,
  `descricao` varchar(200) NOT NULL,
  `cliente_idcliente` int(11) NOT NULL,
  PRIMARY KEY (`idequipamento`),
  KEY `fk_equipamento_cliente` (`cliente_idcliente`),
  CONSTRAINT `fk_equipamento_cliente` FOREIGN KEY (`cliente_idcliente`) REFERENCES `cliente` (`idcliente`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipamento`
--

LOCK TABLES `equipamento` WRITE;
/*!40000 ALTER TABLE `equipamento` DISABLE KEYS */;
INSERT INTO `equipamento` VALUES (101,'Notebook','Dell','Inspiron 15','SN-DELL-001','Notebook não liga corretamente',101),(102,'Impressora','HP','LaserJet P1102','SN-HP-002','Impressora com falha de impressão',102),(103,'Celular','Samsung','Galaxy A52','SN-SAM-003','Celular com problema no conector de carga',103);
/*!40000 ALTER TABLE `equipamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `especialidade`
--

DROP TABLE IF EXISTS `especialidade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `especialidade` (
  `idespecialidade` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `descricao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idespecialidade`),
  UNIQUE KEY `uk_especialidade_nome` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `especialidade`
--

LOCK TABLES `especialidade` WRITE;
/*!40000 ALTER TABLE `especialidade` DISABLE KEYS */;
INSERT INTO `especialidade` VALUES (1,'Notebook','Manutenção de notebooks e ultrabooks'),(2,'Desktop','Manutenção de Desktop'),(3,'Impressora','Manutenção de impressoras jato de tinta e laser'),(4,'Celular','Manutenção básica de smartphones'),(5,'Rede','Manutenção de redes e conectividade');
/*!40000 ALTER TABLE `especialidade` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estado`
--

DROP TABLE IF EXISTS `estado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estado` (
  `idestado` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `sigla` char(2) NOT NULL,
  PRIMARY KEY (`idestado`),
  UNIQUE KEY `uk_estado_sigla` (`sigla`)
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estado`
--

LOCK TABLES `estado` WRITE;
/*!40000 ALTER TABLE `estado` DISABLE KEYS */;
INSERT INTO `estado` VALUES (101,'São Paulo','SP'),(102,'Minas Gerais','MG'),(103,'Rio de Janeiro','RJ');
/*!40000 ALTER TABLE `estado` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `garantia`
--

DROP TABLE IF EXISTS `garantia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `garantia` (
  `idgarantia` int(11) NOT NULL AUTO_INCREMENT,
  `data_inicio` datetime NOT NULL,
  `data_fim` datetime NOT NULL,
  `termo` varchar(255) NOT NULL,
  `ativo` tinyint(4) DEFAULT 1,
  `ordem_servico_idordem_servico` int(11) NOT NULL,
  PRIMARY KEY (`idgarantia`),
  UNIQUE KEY `uk_garantia_ordem_servico` (`ordem_servico_idordem_servico`),
  CONSTRAINT `fk_garantia_ordem_servico` FOREIGN KEY (`ordem_servico_idordem_servico`) REFERENCES `ordem_servico` (`idordem_servico`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `chk_garantia_datas` CHECK (`data_fim` >= `data_inicio`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `garantia`
--

LOCK TABLES `garantia` WRITE;
/*!40000 ALTER TABLE `garantia` DISABLE KEYS */;
INSERT INTO `garantia` VALUES (1,'2026-06-10 08:46:32','2026-09-08 08:46:32','Garantia de 90 dias para o serviço realizado no notebook.',1,1),(2,'2026-06-10 08:46:32','2026-08-09 08:46:32','Garantia de 60 dias para manutenção da impressora.',1,2),(3,'2026-06-10 08:46:32','2026-07-10 08:46:32','Garantia de 30 dias para troca do conector de carga.',1,3);
/*!40000 ALTER TABLE `garantia` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movimentacao_estoque`
--

DROP TABLE IF EXISTS `movimentacao_estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movimentacao_estoque` (
  `idmovimentacao_estoque` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_movimentacao` varchar(45) NOT NULL,
  `quantidade` int(11) NOT NULL,
  `valor_unitario` decimal(10,2) NOT NULL DEFAULT 0.00,
  `data_movimentacao` datetime NOT NULL DEFAULT current_timestamp(),
  `observacao` varchar(255) DEFAULT NULL,
  `peca_idpeca` int(11) NOT NULL,
  `ordem_servico_idordem_servico` int(11) DEFAULT NULL,
  `tecnico_idtecnico` int(11) DEFAULT NULL,
  PRIMARY KEY (`idmovimentacao_estoque`),
  KEY `fk_movimentacao_estoque_peca` (`peca_idpeca`),
  KEY `fk_movimentacao_estoque_ordem_servico` (`ordem_servico_idordem_servico`),
  KEY `fk_movimentacao_estoque_tecnico` (`tecnico_idtecnico`),
  CONSTRAINT `fk_movimentacao_estoque_ordem_servico` FOREIGN KEY (`ordem_servico_idordem_servico`) REFERENCES `ordem_servico` (`idordem_servico`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `fk_movimentacao_estoque_peca` FOREIGN KEY (`peca_idpeca`) REFERENCES `peca` (`idpeca`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_movimentacao_estoque_tecnico` FOREIGN KEY (`tecnico_idtecnico`) REFERENCES `tecnico` (`idtecnico`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `chk_movimentacao_estoque_quantidade` CHECK (`quantidade` > 0),
  CONSTRAINT `chk_movimentacao_estoque_valor` CHECK (`valor_unitario` >= 0),
  CONSTRAINT `chk_movimentacao_estoque_tipo` CHECK (`tipo_movimentacao` in ('ENTRADA','SAIDA','AJUSTE_ENTRADA','AJUSTE_SAIDA'))
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movimentacao_estoque`
--

LOCK TABLES `movimentacao_estoque` WRITE;
/*!40000 ALTER TABLE `movimentacao_estoque` DISABLE KEYS */;
INSERT INTO `movimentacao_estoque` VALUES (1,'SAIDA',1,180.00,'2026-06-10 08:46:32','Bateria utilizada na OS 1',1,1,1),(2,'SAIDA',1,380.00,'2026-06-10 08:46:32','Tela utilizada na OS 2',2,2,7),(3,'SAIDA',1,70.00,'2026-06-10 08:46:32','Conector de carga utilizado na OS 3',5,3,7);
/*!40000 ALTER TABLE `movimentacao_estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ordem_servico`
--

DROP TABLE IF EXISTS `ordem_servico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ordem_servico` (
  `idordem_servico` int(11) NOT NULL AUTO_INCREMENT,
  `defeito_relatado` text NOT NULL,
  `preco` decimal(10,2) DEFAULT 0.00,
  `data_abertura` datetime NOT NULL DEFAULT current_timestamp(),
  `data_fechamento` datetime DEFAULT NULL,
  `data_prevista` datetime DEFAULT NULL,
  `descricao_manutencao` text DEFAULT NULL,
  `status_idstatus` int(11) NOT NULL,
  `tecnico_idtecnico` int(11) DEFAULT NULL,
  `equipamento_idequipamento` int(11) NOT NULL,
  PRIMARY KEY (`idordem_servico`),
  KEY `fk_ordem_servico_status` (`status_idstatus`),
  KEY `fk_ordem_servico_tecnico` (`tecnico_idtecnico`),
  KEY `fk_ordem_servico_equipamento` (`equipamento_idequipamento`),
  CONSTRAINT `fk_ordem_servico_equipamento` FOREIGN KEY (`equipamento_idequipamento`) REFERENCES `equipamento` (`idequipamento`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_ordem_servico_status` FOREIGN KEY (`status_idstatus`) REFERENCES `status_ordem_servico` (`idstatus`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_ordem_servico_tecnico` FOREIGN KEY (`tecnico_idtecnico`) REFERENCES `tecnico` (`idtecnico`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `chk_ordem_servico_preco` CHECK (`preco` >= 0)
) ENGINE=InnoDB AUTO_INCREMENT=204 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ordem_servico`
--

LOCK TABLES `ordem_servico` WRITE;
/*!40000 ALTER TABLE `ordem_servico` DISABLE KEYS */;
INSERT INTO `ordem_servico` VALUES (1,'Cliente informou que o notebook não liga.',250.00,'2026-06-10 08:43:11',NULL,'2026-06-13 08:43:38','Diagnóstico inicial em andamento.',1,1,101),(2,'Impressora apresenta manchas nas folhas.',180.00,'2026-06-10 08:43:11',NULL,'2026-06-12 08:43:38','Será feita limpeza e verificação do toner.',2,7,102),(3,'Celular não carrega corretamente.',120.00,'2026-06-10 08:43:11','2026-06-10 08:43:38','2026-06-10 08:43:38','Conector de carga substituído e aparelho testado.',3,7,103),(202,'Teste',12000.00,'2026-06-15 11:24:27',NULL,NULL,'teste',1,1,101),(203,'Teste de defeito aparelho.',10.00,'2026-06-15 16:35:19',NULL,NULL,'Teste 2 ',1,1,101);
/*!40000 ALTER TABLE `ordem_servico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `peca`
--

DROP TABLE IF EXISTS `peca`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `peca` (
  `idpeca` int(11) NOT NULL AUTO_INCREMENT,
  `codigo` varchar(45) DEFAULT NULL,
  `nome` varchar(100) NOT NULL,
  `descricao` varchar(255) NOT NULL,
  `unidade` varchar(45) DEFAULT 'UN',
  `estoque_atual` int(11) NOT NULL DEFAULT 0,
  `estoque_minimo` int(11) NOT NULL DEFAULT 0,
  `valor_custo` decimal(10,2) NOT NULL DEFAULT 0.00,
  `valor_venda` decimal(10,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`idpeca`),
  UNIQUE KEY `uk_peca_codigo` (`codigo`),
  CONSTRAINT `chk_peca_valores` CHECK (`estoque_atual` >= 0 and `estoque_minimo` >= 0 and `valor_custo` >= 0 and `valor_venda` >= 0)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `peca`
--

LOCK TABLES `peca` WRITE;
/*!40000 ALTER TABLE `peca` DISABLE KEYS */;
INSERT INTO `peca` VALUES (1,'BAT001','Bateria','Bateria para notebook','UN',10,2,120.00,180.00),(2,'TEL001','Tela','Tela de notebook 15.6 polegadas','UN',5,1,250.00,380.00),(3,'TEC001','Teclado','Teclado para notebook','UN',8,2,80.00,140.00),(4,'FON001','Fonte','Fonte de alimentação para notebook','UN',12,3,70.00,120.00),(5,'CON001','Conector de carga','Conector de carga para notebook ou celular','UN',15,3,30.00,70.00),(6,'PC-0002','Placa SUB','Teste Peça','UN',2,1,100.00,120.00);
/*!40000 ALTER TABLE `peca` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `peca_has_ordem_servico`
--

DROP TABLE IF EXISTS `peca_has_ordem_servico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `peca_has_ordem_servico` (
  `peca_idpeca` int(11) NOT NULL,
  `ordem_servico_idordem_servico` int(11) NOT NULL,
  `quantidade` int(11) NOT NULL DEFAULT 1,
  `subtotal` decimal(10,2) NOT NULL DEFAULT 0.00,
  `valor_unitario` decimal(10,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`peca_idpeca`,`ordem_servico_idordem_servico`),
  KEY `fk_peca_has_ordem_servico_ordem_servico` (`ordem_servico_idordem_servico`),
  CONSTRAINT `fk_peca_has_ordem_servico_ordem_servico` FOREIGN KEY (`ordem_servico_idordem_servico`) REFERENCES `ordem_servico` (`idordem_servico`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_peca_has_ordem_servico_peca` FOREIGN KEY (`peca_idpeca`) REFERENCES `peca` (`idpeca`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `chk_peca_has_ordem_servico_valores` CHECK (`quantidade` > 0 and `subtotal` >= 0 and `valor_unitario` >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `peca_has_ordem_servico`
--

LOCK TABLES `peca_has_ordem_servico` WRITE;
/*!40000 ALTER TABLE `peca_has_ordem_servico` DISABLE KEYS */;
INSERT INTO `peca_has_ordem_servico` VALUES (1,1,1,180.00,180.00),(2,2,1,380.00,380.00),(5,3,1,70.00,70.00);
/*!40000 ALTER TABLE `peca_has_ordem_servico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rua`
--

DROP TABLE IF EXISTS `rua`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rua` (
  `idrua` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `bairro_idbairro` int(11) NOT NULL,
  PRIMARY KEY (`idrua`),
  KEY `fk_rua_bairro` (`bairro_idbairro`),
  CONSTRAINT `fk_rua_bairro` FOREIGN KEY (`bairro_idbairro`) REFERENCES `bairro` (`idbairro`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rua`
--

LOCK TABLES `rua` WRITE;
/*!40000 ALTER TABLE `rua` DISABLE KEYS */;
INSERT INTO `rua` VALUES (101,'Rua das Acácias',101),(102,'Avenida Brasil',102),(103,'Rua Moreira César',103);
/*!40000 ALTER TABLE `rua` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `status_ordem_servico`
--

DROP TABLE IF EXISTS `status_ordem_servico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `status_ordem_servico` (
  `idstatus` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `descricao` varchar(255) NOT NULL,
  `finalizado` tinyint(4) DEFAULT 0,
  PRIMARY KEY (`idstatus`),
  UNIQUE KEY `uk_status_nome` (`nome`)
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `status_ordem_servico`
--

LOCK TABLES `status_ordem_servico` WRITE;
/*!40000 ALTER TABLE `status_ordem_servico` DISABLE KEYS */;
INSERT INTO `status_ordem_servico` VALUES (1,'Aberta','Ordem de serviço recém-criada',0),(2,'Em análise','Equipamento em diagnóstico técnico',0),(3,'Em manutenção','Equipamento em processo de manutenção',0),(4,'Aguardando peça','Serviço aguardando peça para continuidade',0),(5,'Finalizada','Ordem de serviço concluída',1),(6,'Cancelada','Ordem de serviço cancelada',1);
/*!40000 ALTER TABLE `status_ordem_servico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tecnico`
--

DROP TABLE IF EXISTS `tecnico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tecnico` (
  `idtecnico` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `senha_hash` varchar(255) NOT NULL,
  `ativo` tinyint(4) DEFAULT 1,
  PRIMARY KEY (`idtecnico`),
  UNIQUE KEY `uk_tecnico_email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=104 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tecnico`
--

LOCK TABLES `tecnico` WRITE;
/*!40000 ALTER TABLE `tecnico` DISABLE KEYS */;
INSERT INTO `tecnico` VALUES (1,'Administrador','admin@teste.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1),(7,'Emily','emily@teste.com','9e351b07323470bb055ee199720d0a080c08a28c0d76488b7d797890582a726b',1),(101,'João Técnico','joao@assistencia.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1),(102,'Ana Manutenção','ana@assistencia.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1),(103,'Pedro Suporte','pedro@assistencia.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92',1);
/*!40000 ALTER TABLE `tecnico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tecnico_has_especialidade`
--

DROP TABLE IF EXISTS `tecnico_has_especialidade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tecnico_has_especialidade` (
  `tecnico_idtecnico` int(11) NOT NULL,
  `especialidade_idespecialidade` int(11) NOT NULL,
  PRIMARY KEY (`tecnico_idtecnico`,`especialidade_idespecialidade`),
  KEY `fk_tecnico_has_especialidade_especialidade` (`especialidade_idespecialidade`),
  CONSTRAINT `fk_tecnico_has_especialidade_especialidade` FOREIGN KEY (`especialidade_idespecialidade`) REFERENCES `especialidade` (`idespecialidade`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `fk_tecnico_has_especialidade_tecnico` FOREIGN KEY (`tecnico_idtecnico`) REFERENCES `tecnico` (`idtecnico`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tecnico_has_especialidade`
--

LOCK TABLES `tecnico_has_especialidade` WRITE;
/*!40000 ALTER TABLE `tecnico_has_especialidade` DISABLE KEYS */;
INSERT INTO `tecnico_has_especialidade` VALUES (7,1),(7,2);
/*!40000 ALTER TABLE `tecnico_has_especialidade` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'assistencia_tecnica'
--

--
-- Dumping routines for database 'assistencia_tecnica'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-06-15 18:52:24
