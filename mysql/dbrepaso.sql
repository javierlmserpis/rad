CREATE DATABASE dbrepaso;

USE dbrepaso;

CREATE TABLE articulo (
  id int(11) PRIMARY KEY NOT NULL AUTO_INCREMENT,
  nombre varchar(45) NOT NULL,
  categoria int(11),
  precio decimal(10,2)
 ) ;

CREATE TABLE categoria (
  id int(11) PRIMARY KEY NOT NULL AUTO_INCREMENT,
  nombre varchar(45) NOT NULL
  ) 