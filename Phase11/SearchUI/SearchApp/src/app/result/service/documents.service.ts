import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Document } from '../models/document';

@Injectable()
export class DocumentsService {

  private readonly url = "http://localhost:5000/SearchEngine/"

  constructor(private http: HttpClient) { }

  public async getDocuments(query: string): Promise<Document[]> {
    let ids = await this.http.get<string[]>(this.url,
      { params: { "input": query } })
      .toPromise()
    let results : Document[] = []
    for (const x of ids) {
      await this.http.get<Document>(this.url + x)
        .toPromise()
        .then(data => results.push(data))
    }
    return results
  }
}
