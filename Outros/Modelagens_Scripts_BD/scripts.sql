
CREATE TABLE produto (
	id_produto INT NOT NULL IDENTITY(1, 1),
	descricao VARCHAR(100) NOT NULL UNIQUE,
	preco_unitario NUMERIC(7, 2) NOT NULL CHECK (preco_unitario > 0),
	saldo_estoque INT NOT NULL CHECK (saldo_estoque >= 0),
	PRIMARY KEY(id_produto)
);

CREATE TABLE venda (
	id_venda INT NOT NULL IDENTITY(1, 1), 
	data_venda DATE NULL DEFAULT getdate(),
	nome_cliente varchar(100) NOT NULL,
	vlr_total numeric(10,2) NOT NULL CHECK (vlr_total > 0),
	PRIMARY KEY(id_venda)
);

CREATE TABLE item_venda (
	id_venda INT NOT NULL,
	id_produto INT NOT NULL,
	qtde INT NOT NULL CHECK (qtde > 0),
	preco_unitario NUMERIC(7,2) NOT NULL,
	vlr_total_item NUMERIC(8,2) NOT NULL CHECK (vlr_total_item > 0),
	PRIMARY KEY(id_venda, id_produto),
	FOREIGN KEY(id_venda) REFERENCES venda(id_venda) ON DELETE CASCADE,
	FOREIGN KEY(id_produto) REFERENCES produto(id_produto)
);


-- Trigers
CREATE TRIGGER trg_update_saldo_produto
ON item_venda
AFTER INSERT
AS
BEGIN
    -- Atualiza o saldo de estoque de cada produto conforme o item de venda inserido
    UPDATE produto
    SET saldo_estoque = saldo_estoque - inserted.qtde
    FROM produto
    INNER JOIN inserted ON produto.id_produto = inserted.id_produto
    WHERE inserted.qtde > 0;

    -- Verifica se algum produto ficou com saldo negativo após a atualização
    IF EXISTS (
        SELECT 1 
        FROM produto 
        WHERE saldo_estoque < 0
    )
    BEGIN
        -- Se houver saldo negativo, desfaz a operação e lança um erro
        ROLLBACK TRANSACTION;
        THROW 50000, 'Não é permitido realizar a venda. Saldo insuficiente no estoque.', 1;
    END
END;