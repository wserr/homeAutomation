use serde::Deserialize;

#[derive(Deserialize, Debug, Default)]
pub enum Mode {
    #[default]
    ReadWeatherData,
}
