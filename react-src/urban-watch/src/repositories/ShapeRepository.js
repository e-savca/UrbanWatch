import API_KEY from '../data/ApiKey.js'

const BASE_URL = 'https://api.tranzy.ai/v1/opendata'
function getUrl(shapeId) {
  return `${BASE_URL}/shapes?shape_id=${shapeId}`
}

export default class ShapeRepository {
  async GetShapeById(shapeId) {
    const url = getUrl(shapeId)
    const options = {
      method: 'GET',
      headers: {
        'X-Agency-Id': '4',
        Accept: 'application/json',
        'X-API-KEY': `${API_KEY}`,
      },
    }

    try {
      const response = await fetch(url, options)
      const data = await response.json()
      console.log(data)
      return data
    } catch (error) {
      console.error(error)
    }
  }
}
