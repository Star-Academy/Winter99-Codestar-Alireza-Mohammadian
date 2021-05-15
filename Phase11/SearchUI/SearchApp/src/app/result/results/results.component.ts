import { Component, Input, OnInit } from '@angular/core';
import { Document } from '../models/document';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnInit {

  @Input()
  public documents: Document[] = []

  constructor() { }

  ngOnInit(): void {
  }
}
