create database dashboard_voiture;
use dashboard_voiture;

create table voiture(
    id integer auto_increment primary key,
    nom varchar(255) not null,
    acceleration float not null,
    deceleration float not null,
    vitesse float not null,
    capaciteReservoir float,
    vitesseMax FLOAT,
    consommationMax float
);

create table enregistrement(
    id integer auto_increment primary key,
    idVoiture integer,
    vitesse float not null,
    acceleration float not null,
    t integer not null,
    heure time default current_timestamp,
    foreign key (idVoiture) references voiture(id)
);

INSERT INTO voiture (nom, acceleration, deceleration, vitesse, capaciteReservoir, consommationMax, vitesseMax)
VALUES
('Alea 1', 50, 20, 0, 1, 0.05, 250.0);
-- Acc�l�ration en km/h/s

-- select vitesse from voiture where id=? union (select coalesce(max(vitesseInitiale),0) vitesse from enregistrement where idVoiture = ?) order by vitesse limit 1;

--select vitesse from enregistrement where idVoiture = @idVoiture order by id desc limit 1;

--select vitesse from voiture where id = @id