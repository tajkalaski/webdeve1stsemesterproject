import { Component, OnInit } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { ActivatedRoute, UrlSegment } from '@angular/router';
import { timer } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'utility-tools',
  animations: [
    trigger('slideUpDown',
    [
      transition(':enter', [
        style({ opacity: 0 }),
        animate(500, style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate(500, style({ opacity: 0 }))
      ])
    ])
  ],
  templateUrl: './utility-tools.component.html',
  styleUrls: ['./utility-tools.component.scss']
})
export class UtilityToolsComponent implements OnInit {
  currentRoute: string;
  selectedTool: string;
  toolBoxExpanded = false;
  tools = ['Metode', 'Noter', 'Løbende kommentarer', 'Hjælp', 'Dokumenter'];

  constructor(
    private route: ActivatedRoute
  ) { }

  changeTool(tool?: string) {
    if (tool) { return this.selectedTool = tool; }
    this.selectedTool = undefined;
  }

  getUrl() {
    this.route.firstChild.url.subscribe((res: UrlSegment[]) => this.currentRoute = res[0].path);
  }

  ngOnInit() {
    const interval$ = timer(150);
    interval$.pipe(
      switchMap(() => {
        return this.route.firstChild.url
          .pipe(
            map((url: any[]) => {
              if (url.length !== 0) { this.currentRoute = url[0].path; }
            })
          );
      }))
      .subscribe();
  }
}
