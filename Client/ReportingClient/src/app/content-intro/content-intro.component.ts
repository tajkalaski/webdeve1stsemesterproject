import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'content-intro',
  templateUrl: './content-intro.component.html',
  styleUrls: ['./content-intro.component.scss']
})
export class ContentIntroComponent implements OnInit {
  @Input() title: string;
  @Input() subTitle: string;
  @Input() description: string;

  constructor() { }

  ngOnInit() {
  }

}
