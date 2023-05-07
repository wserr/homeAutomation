use crate::mode::Mode;
use serde::Deserialize;

#[derive(Deserialize, Debug)]
pub struct Input {
    #[serde(default)]
    pub program_mode: Mode,
    pub weather_api_base_url: String,
    pub weather_api_key: String,
    pub influx_db_base_url: String,
    pub influx_db_token: String,
    pub latitude: String,
    pub longitude: String,
}
