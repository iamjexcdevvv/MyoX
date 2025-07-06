import { UserAuth } from "./user-auth.model";

export interface UserRegister extends UserAuth {
    confirmPassword: string | null
}