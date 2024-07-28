import { EStatus } from "./estatus.enum";

// src/app/core/models/categoria.model.ts
export interface Categoria {
    id: string;
    nome: string;
    status: EStatus;
}

export interface CriarCategoriaDTO {
    nome: string;
}

export interface EditarCategoriaDTO {
    id: string;
    nome: string;
    status: EStatus;
}
