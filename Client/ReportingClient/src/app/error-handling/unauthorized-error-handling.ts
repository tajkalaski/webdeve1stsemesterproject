import { AuthService } from './../services/auth.service';
import { ErrorHandler, Injectable } from "@angular/core";

@Injectable()
export class UnauthorizedErrorHandling implements ErrorHandler {

    constructor(
        private auth: AuthService,
    ) {}

    handleError(error) {
      if (error.status == 401) {
        // console.log("error handler reached");
        // console.log(error);
        this.auth.logout();
      }
    }
  }