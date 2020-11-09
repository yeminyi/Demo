import { Injectable } from '@angular/core';
import { BaseService } from '../../shared/base.service';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { ScoreParameters } from '../models/score-parameters';
import { Score } from '../models/score';
import { ScoreAdd } from '../models/score-add';
import { Observable } from 'rxjs';
import { Team } from '../models/team';
import { Game } from '../models/Game';
@Injectable({
  providedIn: 'root'
})
export class ScoreService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }
  
  getList(postParameter?: any | ScoreParameters): Observable<HttpResponse<Array<Score>>> {
    const uri = `${this.apiUrlBase}/scores`;
    return this.http.get<Array<Score>>(
      uri, {
        observe: 'response',
      params: postParameter
      });
  }
  addScore(post: ScoreAdd) {
       
    return this.http.post<Score>(`${this.apiUrlBase}/scores`, post);
 
  }
  getTeamList(): Observable<HttpResponse<Array<Team>>> {
    const uri = `${this.apiUrlBase}/teams`;
    return this.http.get<Array<Team>>(
      uri, {
        observe: 'response'
      });
  }

  getGameList(): Observable<HttpResponse<Array<Game>>> {
    const uri = `${this.apiUrlBase}/games`;
    return this.http.get<Array<Game>>(
      uri, {
        observe: 'response'
      });
  }

}
