import { EStatus } from "../../../core/models/status.enum";

export interface Categoria {
    id: string;
    nome: string;
    status: EStatus;
}
