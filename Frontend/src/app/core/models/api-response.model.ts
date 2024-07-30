export interface ApiResponse<T> {
    success: boolean;
    statusCode: number;
    data: T | null;
    errors: string[];
}
