-------------------------------------
--SCHEMA CREATION
-------------------------------------
CREATE SCHEMA HGS DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
-------------------------------------



-------------------------------------
--TABLES CREATION
-------------------------------------
CREATE TABLE area(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    description VARCHAR(255) NOT NULL
);

CREATE TABLE branch(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    municipality VARCHAR(100) NOT NULL
);

CREATE TABLE areasucursal(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    area_id INTEGER NOT NULL,
    branch_id INTEGER NOT NULL,    

    FOREIGN KEY (area_id) REFERENCES Area(id),
    FOREIGN KEY (branch_id) REFERENCES Branch(id)
);

CREATE TABLE bed(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    areaSucursal_id INTEGER NOT NULL,
    size VARCHAR(15) NOT NULL,
    annotations VARCHAR(255) NULL,
    state BIT NOT NULL,

    FOREIGN KEY (areaSucursal_id) REFERENCES AreaSucursal(id)
);

CREATE TABLE patient(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    dpi VARCHAR(13) NOT NULL,
    name VARCHAR(20) NOT NULL,
    lastname VARCHAR(20) NOT NULL,
    birthdate DATE NOT NULL,
    observations TEXT NULL
);
---------------------
CREATE TABLE speciality(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(60) NOT NULL
);

CREATE TABLE doctor(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    collegiateNumber VARCHAR(9) NOT NULL,
    user VARCHAR(30) NOT NULL,
    password VARCHAR(30) NOT NULL,
    dpi VARCHAR(13) NOT NULL,
    name VARCHAR(20) NOT NULL,
    lastname VARCHAR(20) NOT NULL,
    birthdate DATE NOT NULL,
    specialty_id INTEGER NOT NULL,

    FOREIGN KEY (specialty_id) REFERENCES Speciality(id)
);
---------------------
CREATE TABLE bedpatient(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    bed_id INTEGER NOT NULL,
    patient_id INTEGER NOT NULL,
    reason TEXT NOT NULL,
    state BIT NOT NULL,
    doctor_id INTEGER NOT NULL,
    annotations TEXT NULL,
    startDate DATE NOT NULL,
    endDate DATE NULL,

    FOREIGN KEY (bed_id) REFERENCES Bed(id),
    FOREIGN KEY (patient_id) REFERENCES Patient(id),
    FOREIGN KEY (doctor_id) REFERENCES Doctor(id)
);

CREATE TABLE administrator(
    id INTEGER PRIMARY KEY AUTO_INCREMENT,
    user VARCHAR(30) NOT NULL,
    password VARCHAR(30) NOT NULL
);
-------------------------------------