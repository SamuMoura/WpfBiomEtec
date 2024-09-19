CREATE DATABASE BiomEtec;
USE BiomEtec;

CREATE TABLE turma (
    idTurma INT(2) PRIMARY KEY NOT NULL,
    turma VARCHAR(4) NOT NULL,
    turno VARCHAR(10)
);
-- Os IDs de turma seguem a lógica de 1MN = 01, 1MAA = 02, 1DSA = 03 e assim seguindo para 2s e 3s.

CREATE TABLE alunos (
    RM INT(5) AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(60) NOT NULL,
    biometria VARCHAR(300) NOT NULL,
    email VARCHAR(80) NOT NULL,
    CPF VARCHAR(14) NOT NULL,
    telefone VARCHAR(14) NOT NULL,
    idTurma INT(2),
    CONSTRAINT fk_alunos_turma_idTurma FOREIGN KEY(idTurma) REFERENCES turma(idTurma)
);
-- Adição após o Auto Increment 
ALTER TABLE alunos AUTO_INCREMENT = 24001;

CREATE TABLE responsaveis (
    CPF VARCHAR(11) PRIMARY KEY NOT NULL,
    nome VARCHAR(60) NOT NULL,
    email VARCHAR(60),
    telefone VARCHAR(11),
    biometria VARCHAR(300) NOT NULL,
    relacionamentoaluno VARCHAR(10) NOT NULL,
    RM INT(5),
    CONSTRAINT fk_responsaveis_alunos_RM FOREIGN KEY(RM) REFERENCES alunos(RM)
);

CREATE TABLE catraca (
    idCatraca INT(2) PRIMARY KEY NOT NULL,
    localizacao VARCHAR(10)
);

CREATE TABLE usuario (
    usuario VARCHAR(10) PRIMARY KEY NOT NULL,
    senha VARCHAR(8) NOT NULL,
    permissao ENUM('admin', 'professor', 'segurança') NOT NULL
);

CREATE TABLE acesso (
    id_acesso INT AUTO_INCREMENT PRIMARY KEY,
    data_hora_entrada DATETIME NOT NULL,
    data_hora_saida DATETIME,
    usuario VARCHAR(10),
    RM INT(5),
    idCatraca INT(2),
    CONSTRAINT fk_acesso_alunos_RM FOREIGN KEY(RM) REFERENCES alunos(RM),
    CONSTRAINT fk_acesso_catraca_idCatraca FOREIGN KEY(idCatraca) REFERENCES catraca(idCatraca),
    CONSTRAINT fk_acesso_usuario_usuario FOREIGN KEY(usuario) REFERENCES usuario(usuario)
);
-- Adição após o Auto Increment 
ALTER TABLE acesso AUTO_INCREMENT = 1;

/* Inserção de usuários */
INSERT INTO usuario (usuario, senha, permissao) VALUES ('adm', '000000', 'admin');
INSERT INTO usuario (usuario, senha, permissao) VALUES ('pedrot', '12345678', 'segurança');

/* Inserção de turmas */
INSERT INTO turma (idTurma, turma, turno) VALUES (9, '2DSA', 'Integral');
INSERT INTO turma (idTurma, turma, turno) VALUES (6, '2MN', 'Integral');
INSERT INTO turma (idTurma, turma, turno) VALUES (7, '2MAA', 'Integral');

/* Inserção de alunos */
INSERT INTO alunos (RM, nome, biometria, idTurma, email, CPF, telefone) VALUES (23001, 'João Silva', 'biometria1', 7, 'jão@email.com', '111.222.333-00', '(19)91111-2222');
INSERT INTO alunos (RM, nome, biometria, idTurma, email, CPF, telefone) VALUES (23242, 'Victoria Santos', 'biometria2', 6, 'vic@email.com', '222.333.444-00', '(19)92222-3333');
INSERT INTO alunos (RM, nome, biometria, idTurma, email, CPF, telefone) VALUES (23023, 'Nilson Ortiz', 'biometria3', 9, 'nilson@email.com', '333.444.555-00', '(19)93333-4444');

/* Inserção de responsáveis */
INSERT INTO responsaveis (CPF, nome, email, telefone, biometria, relacionamentoaluno, RM) VALUES ('12345678901', 'Carlos Silva', 'carlos@email.com', '19987654321', 'biometria1', 'Pai', 23001);
INSERT INTO responsaveis (CPF, nome, email, telefone, biometria, relacionamentoaluno, RM) VALUES ('23456789012', 'Ana Santos', 'ana@email.com', '19987654322', 'biometria2', 'Mãe', 23242);
INSERT INTO responsaveis (CPF, nome, email, telefone, biometria, relacionamentoaluno, RM) VALUES ('34567890123', 'Rodrigo Ortiz', 'marcos@email.com', '19987654323', 'biometria3', 'Pai', 23023);

/* Inserção de catracas */
INSERT INTO catraca (idCatraca, localizacao) VALUES (1, 'Principal');
INSERT INTO catraca (idCatraca, localizacao) VALUES (2, 'Lateral');

/* Inserção de acessos (exemplo de registros de presença) */
INSERT INTO acesso (id_acesso, data_hora_entrada, data_hora_saida, usuario, RM, idCatraca) VALUES (1, '2024-09-01 08:00:00', '2024-09-01 15:00:00', 'pedrot', 23001, 1);
INSERT INTO acesso (id_acesso, data_hora_entrada, data_hora_saida, usuario, RM, idCatraca) VALUES (2, '2024-09-01 08:15:00', '2024-09-01 12:30:00', 'pedrot', 23242, 2);
INSERT INTO acesso (id_acesso, data_hora_entrada, data_hora_saida, usuario, RM, idCatraca) VALUES (3, '2024-09-01 08:30:00', NULL, 'pedrot', 23001, 1); -- Entrada sem saída
INSERT INTO acesso (id_acesso, data_hora_entrada, data_hora_saida, usuario, RM, idCatraca) VALUES (4, '2024-09-01 08:45:00', NULL, 'pedrot', 23242, 2); -- Entrada sem saída
INSERT INTO acesso (id_acesso, data_hora_entrada, data_hora_saida, usuario, RM, idCatraca) VALUES (5, '2024-09-02 09:00:00', '2024-09-02 16:00:00', 'pedrot', 23023, 1);


/* Consultas */
SELECT * FROM alunos;
SELECT * FROM responsaveis;
SELECT * FROM turma;
SELECT * FROM catraca;
SELECT * FROM acesso;
SELECT * FROM usuario;

/* Inner Join para relatórios */
-- RM Alunos-Responsáveis
SELECT alunos.*, responsaveis.* FROM alunos INNER JOIN responsaveis ON alunos.RM = responsaveis.RM;

-- idTurma Turma-Alunos
SELECT turma.*, alunos.* FROM turma INNER JOIN alunos ON turma.idTurma = alunos.idTurma;

-- idCatraca Catraca-Acesso
SELECT catraca.*, acesso.* FROM catraca INNER JOIN acesso ON catraca.idCatraca = acesso.idCatraca;

-- RM Alunos-Acesso
SELECT alunos.*, acesso.* FROM alunos INNER JOIN acesso ON alunos.RM = acesso.RM;

-- usuario Usuário-Acesso
SELECT usuario.*, acesso.* FROM usuario INNER JOIN acesso ON usuario.usuario = acesso.usuario;

/* Relatório de acessos */
CREATE VIEW relatorio_acessos AS
SELECT RM, data_hora_entrada, data_hora_saida, usuario
FROM acesso;

SELECT * FROM relatorio_acessos;

/* Relatório de presenças por turma */
CREATE VIEW relatorio_presencas_turma AS
SELECT t.idTurma, t.turma, t.turno, COUNT(ac.id_acesso) AS total_presencas
FROM turma t
LEFT JOIN alunos a ON t.idTurma = a.idTurma
LEFT JOIN acesso ac ON a.RM = ac.RM
WHERE ac.data_hora_saida IS NULL
GROUP BY t.idTurma;

SELECT * FROM relatorio_presencas_turma;

/*Presenças com filtro de turmas*/

/* MN */ SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = ('1MN', '2MN', '3MN');
/* 1°MN */ SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '1MN';
/* 2°MN */ SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '2MN';
/* 3°MN */ SELECT COUNT(*) FROM relatorio_presencas_turma WHERE turma = '3MN';

/* MA */ SELECT * FROM relatorio_presencas_turma WHERE turma = ('1MAA', '2MAA', '3MAA', '1MAB', '2MAB', '3MAB');
/* 1°MAA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '1MAA';
/* 1°MAB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '1MAB';
/* 2°MAA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '2MAA';
/* 2°MAB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '2MAB';
/* 3°MAA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '3MAA';
/* 3°MAB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '3MAB';

/* DS */ SELECT * FROM relatorio_presencas_turma WHERE turma = ('1DSA', '2DSA', '3DSA', '1DSB', '2DSB', '3DSB');
/* 1°DSA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '1DSA';
/* 1°DSB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '1DSB';
/* 2°DSA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '2DSA';
/* 2°DSB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '2DSB';
/* 3°DSA */ SELECT * FROM relatorio_presencas_turma WHERE turma = '3DSA';
/* 3°DSB */ SELECT * FROM relatorio_presencas_turma WHERE turma = '3DSB';