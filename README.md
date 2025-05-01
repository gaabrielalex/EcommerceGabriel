# EcommerceGabriel

Este é um sistema de gerenciamento de pedidos desenvolvido em **VB.NET**, dividido em várias camadas para facilitar a manutenção e escalabilidade. O projeto segue uma arquitetura modular, com diferentes responsabilidades separadas em pastas e projetos distintos.

## Funcionalidades do Projeto

O sistema oferece as seguintes funcionalidades principais, focadas em operações CRUD (Criar, Ler, Atualizar e Deletar):

1. **Gerenciamento de Pedidos**
   - Criação de novos pedidos.
   - Consulta de pedidos existentes.
   - Atualização de informações de pedidos.
   - Exclusão de pedidos.

2. **Gerenciamento de Itens do Pedido**
   - Adição de itens a um pedido.
   - Consulta de itens associados a um pedido.
   - Atualização de informações dos itens (ex.: quantidade).
   - Remoção de itens de um pedido.

3. **Gerenciamento de Clientes**
   - Cadastro de novos clientes.
   - Consulta de clientes existentes.
   - Atualização de informações dos clientes.
   - Exclusão de registros de clientes.

4. **Gerenciamento de Produtos**
   - Cadastro de novos produtos.
   - Consulta de produtos disponíveis.
   - Atualização de informações dos produtos (ex.: preço, descrição).
   - Exclusão de produtos.

---

## Descrição das Pastas

- **DAOEcommerceGabrielX/** 📂  
  Contém a lógica de acesso a dados (Data Access Object).

- **ModelsEcommerceGabrielX/** 📦  
  Define os modelos e entidades do sistema.

- **ServicesEcommerceGabrielX/** ⚙️  
  Implementa as regras de negócio e serviços do sistema.

- **UtilsEcommerceGabrielX/** 🔧  
  Contém utilitários, como manipuladores de erro e registro de logs.

- **WebEcommerceGabriel/** 🌐  
  Interface web do sistema, incluindo páginas ASP.NET e configurações.

---

## Pré-requisitos

- **Visual Studio** 🖥️  
  Para abrir e compilar a solução `.sln`.

- **.NET Framework** 🛠️  
  Certifique-se de que a versão necessária está instalada.

- **Banco de Dados** 💾  
  Scripts para o banco de dados estão localizados em `Outros/Modelagens_Scripts_BD/`.
