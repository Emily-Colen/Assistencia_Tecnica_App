USE assistencia_tecnica;

INSERT INTO tecnico (nome, email, senha_hash, ativo)
VALUES ('Administrador', 'admin@teste.com', SHA2('123456', 256), 1);

SELECT idtecnico, nome, email, ativo
FROM tecnico;
