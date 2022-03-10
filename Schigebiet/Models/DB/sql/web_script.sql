create database schidb collate utf8mb4_general_ci;
use schidb;
create table kunden(
	k_id int unsigned not null auto_increment,
	name varchar(100) not null,
	password varchar(300) not null,
	email varchar(150) null,
	birthdate date null,
	geschlecht int null,
	PRIMARY KEY (k_id)
);
create table tickets(
	t_nr int unsigned not null auto_increment,
	preis double not null,
	t_art int not null,
	kaufzeit date null,
	PRIMARY KEY (t_nr),
	FOREIGN KEY (t_nr) REFERENCES kunden (k_id)
);
insert into kunden values(null, "Noel", sha2("Hallo123", 512), "noel@gmail.com", "2004-02-09", 0);
insert into kunden values(null, "Matteo", sha2("Tschuess123", 512), "matteo@gmail.com", "2004-02-25", 0);
insert into tickets values(null, 10, 5, "2004-02-28");
insert into tickets values(null, 20, 4, "2004-02-10");