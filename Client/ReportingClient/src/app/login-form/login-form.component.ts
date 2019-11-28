import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  constructor(
    private auth: AuthService,
    private router: Router,
  ) { }

  login(email, password) {
    if (email && password) {
      this.auth.login(email, password)
        .subscribe(() => {
          this.router.navigate(['']);
        });
    }
  }

  ngOnInit() {
  }

}
