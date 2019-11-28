import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  isAuthenticated = false;
  subscription: Subscription;
  userLanguage: string;

  constructor(
    private auth: AuthService,
    private translate: TranslateService,
  ) {
    this.translate.setDefaultLang('English');
  }

  ngOnInit() {
    this.subscription = this.auth.authStatus$.subscribe((status: boolean) => this.isAuthenticated = status);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
