CREATE SCHEMA sample AUTHORIZATION demo_user;

CREATE TABLE sample.author (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT author_pk PRIMARY KEY (id)
);

CREATE TABLE sample.genre (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT genre_pk PRIMARY KEY (id)
);

CREATE TABLE sample.book (
	id uuid NOT NULL,
	created timestamp NOT NULL,
	"name" varchar NOT NULL,
	isbn int8 NULL,
	"authorId" uuid NOT NULL,
	"genreId" uuid NULL,
	CONSTRAINT book_pk PRIMARY KEY (id)
);


-- sample.book foreign keys

ALTER TABLE sample.book ADD CONSTRAINT book_fk FOREIGN KEY ("authorId") REFERENCES sample.author(id);
ALTER TABLE sample.book ADD CONSTRAINT book_fk_1 FOREIGN KEY ("genreId") REFERENCES sample.genre(id);