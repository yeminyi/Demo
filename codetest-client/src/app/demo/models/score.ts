import { Entity } from '../../shared/models/entity';

export class Score extends Entity {
    gameTitle: string;
    teamA: string;
    teamB: string;
    teamAscore: Number;
    teamBscore: Number;
    employee: string;
    updateTime: Date;
    
}
