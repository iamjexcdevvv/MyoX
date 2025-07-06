import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserRegister } from "../../shared/models/user-register.model";
import { environment } from "../../../environments/environment";
import { AuthResult } from "../../shared/models/auth-result.model";

@Injectable({ providedIn: "root" })
export class AuthService {
	private readonly _http = inject(HttpClient);

	constructor() {}

	register(request: UserRegister) {
		return this._http.post<AuthResult>(
			`${environment.apiUrl}/Auth/register`,
			request
		);
	}
}
