import { Component, Input, OnInit } from '@angular/core';
import { Document } from '../models/document';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss']
})
export class ResultCardComponent implements OnInit {

  @Input()
  public document?: Document

  constructor() { }

  ngOnInit(): void {
  }

}
